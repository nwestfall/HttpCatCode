# HttpCatCode

https://http.cat is an amazing website with 100% accuracy.  With a small library and 1 line of code, add the `x-cat-status` header to all your requests to let your more advanced users know your API or website means business.

### How does it work
There is a small piece of middleware that sits in your dotnet core web stack.  It will add to every response the `x-cat-status` header with the https://http.cat image that matches the response.  Simple, easy, do it.

### How do I use it
1. `dotnet add package HttpCatCode`
2. Add `using HttpCatCode;` to the top of your `Startup.cs` file
3. Add `app.UseHttpCat();` to the top of your `public void Configure(IApplicationBuilder app, IWebHostEnvironment env)` method
4. Enjoy.

### If you really mean business...
Add `true` as a parameter to `UseHttpCat();` and watch the magic happen on all non `200` requests.