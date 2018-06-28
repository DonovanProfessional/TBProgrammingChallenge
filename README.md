# TBProgrammingChallenge

## Dev Diary Part 1:
In which I get something off the ground.

TODO: Make everything Async, fix JS bugs on front end, mock more of the data abstraction layer out, implement authorization, create a pleasant UI, and sanitize I/O (mostly for XSS, but also for whatever DB ends up being used). Also todo: logging, middleware.

Lots of fun building this up; I hadn't used the Simple Injector for .Net before, and managed to start completely in the wrong place.

To begin, I started with matching up the directory structure with the one provided. This proved to be an interesting experience, since I'd not used the repo pattern or seen a specific structure with the domain/entities and domain/abstract before. I immediately set off to determine if it was a default .net project creation structure, as it would simplify a *lot* of stuff, however, it appeared not to be the case. 

I'd initially planned on using a new EF project for the data layer and business objects, but I assumed the code would need to be portable, and this leaves it possible to run on someone else's PC for the time being. For future dev I'm considering SQLlite for portability. I was hoping I could set up the API to utilize a raw list of output values as a dataset, in EF, but stopped going down that route when I thought that EF's scaffolding might not fit in to the folder structure provided without causing a bunch of errors. Moreoever, I wasn't entirely sure if EF fits the repo pattern. It may have worked with a bit of hammering, but I figured it'd be best to add it in later.

With EF out of the picture (temporarily), I figured I'd try just stubbing out most of the code for the repo layer. From what I read, it was similar to a data abstraction layer, so I pictured what EF was doing, so I stubbed out a mock 'datawrapper' class to contain a 'data' class. In my mind, the data class acts as the raw database/data stream, and the wrapper acts as the accessor, batching requests, sanitizing, and eventually lazily returning result sets, probably using generics and that yield keyword. For the sake of time, it's still accessing the 'DB' synchronously. 

Pulling the data out from the data accessor class wasn't a problem with the web API, however, the next steps for the presentation layer through me for a loop when I tried to start using the simple dependency injector framework.

The example I found (below) of the base DI startup class include a reference to a 'service'. Previously, I'd worked with both web*services* and local windows *services*, so I figured it meant I needed to create a new WCF *service* reference and implementation of it. I'd done this before with SOAP endpoints, but so far I had built out my API to be REST, not SOAP.

 ```container.Register<IUserService, UserService>(Lifestyle.Scoped);```

Eventually, I found out the terminology was overloaded again, and that it meant more of the general concept of a service implementation, rather than an actual soap service implementation. This still has me confused, as the resulting architecture involves what's effectively TWO rest apis now, one in the intended place for the API, and one "service implementation" at the presentation layer. The other way I could think of to register my implementation involved having the intended API export an "integration" package, which would not only give the code multiple responsibilities (bad), but would strongly couple any code that uses the integration package with the API version. I understand that's not always bad, but I was preferring weaker coupling, eg: an un-updated implementation just ignoring new fields, as opposed to breaking entirely due to version mismatch. So since both architectures I thought of lead to dead ends, I'm nearly 100% sure I'm misusing the DI framework, so any guidance is welcome w/ regard to service registration. Bonus points if it ends up with weak coupling to the specifities of the API.

The usage of JQuery to directly hit the (intended)API seems to also go against the idea of having the service implementation registered in the container, so I'm curious as to how grossly I'm misusing the Simple Injector framework here. In my mind, DI means giving the implementation to where it gets used, so just directly hitting the API seems to avoid the service implementation we registered entirely?

End result is my JQuery currently calls both APIs, both the intended one, and the "phantom" API at the presentation project (which uses the service implementation). It's a little buggy but hopefully less so once I hammer out the todos later.

___

## Dev Diary Part 2:
Async where it matters, and browser specific brick walls.

TODO: Make back-end API more async, make presentation nicer, mock more (less?) of the data abstraction out, fix IE duplicates, implement authorization, sanitize I/O, (mostly for XSS, look into CSRF with CORS controls). also todo: logging, middleware, queue for transaction status tracking on back end. 

Mostly spent a lot of time trying figuring out: 

1) Where Async made sense on the back-end. (Not in many places without implementing a queue and token for transaction status tracking; this is still todo)
2) Duplicate JQuery "responses" appearing in internet explorer, but only when dev tools was closed. With dev tools open, everything works fine.
3) JQuery direct API call responses not showing up at all in Firefox.

For the first portion, I implemented an async method on the "phantom" api, which seemed to work well, calling a still synchronous data access api elsewhere.

For the second portion, this was tracked down to either IE caching XHTTP requests, or from console logging in one of the jquery libraries. Still tbd on which.

For the third portion, it turns out this was due to cross-origin-site-request headers not being present. After following the instructions to a 't' on many sites, I failed to get it to work
until I realized it was being called after the simple DI framework populated headers. It works now, but requires some reconfigurations.

