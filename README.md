Hi!

It's a demo web application built with ASP.NET 6.0

Here, I tried to implement some best practices, like:
- separated responsibilities on different app layers;
- using interfaces and implementing dependency injection;
- using Dapper for DB access (I like this library for being able to provide both easy ORM and plain SQL execution);
- the real DB repository class was also replaced with the mock class, so you can try the app without needing actual DB;
- added logging, documentation, and exception handling;
  
