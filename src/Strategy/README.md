## Strategy Pattern - Behavioral

This pattern is useful for swapping implementations at runtime rather than compile time.

## Use Case

We have an API that brews coffee with different methods.

## Problem

This use case is very simple but we can imagine that the response could actually be a complex business algorithm. In our `WithoutStrategy` everything is done in the API controller which causes all that code to be mashed together in one place. If we ever need to change one of the responses we are effectively changing all the responses, plus we are likely to introduce logic that mixes all the cases together which will make it harder to maintain in the future.

## 1. Basic Strategy

In a basic strategy we will be intoducing a few classes to break the logic into multiple classes.

1. The first class we need is a `Context` which keeps a reference to the `Strategy` we are using.
1. After that we will abstract each of the different cases into their own classes that implement a single shared interface.
1. Next, since we are using DI, we can put the context into the `IServiceCollection` in `Startup.cs` and use it in the API controller.

By moving all the logic into the strategy classes we can add new ones, or update existing ones, without worry of breaking the other strategies.

### Problems

- For very trivial examples, such as this, the cost of the new classes might be worse than the cost of the inline code. But this is very rare in any real world example.
- With this pattern we still have the logic for determining which strategy to use in the API controller and each of the strategies have to be instantiated inline, rather than using the built in dependency injector, which comes in very handy when there are more sub-dependencies.

## 2. Dependency Injected Strategy (Basic)

With the basic strategy we still have to instatiate the strategies and have a large switch case that has to be modified everytime we get a new strategy. If we use Dependency Injection we can have the context choose a strategy based on a property given by the stategy itself.

1. Add a property to all the strategies that "defines" it.
1. Add all the strategies to the IoC Container.
1. Update the context to take in the thing that "defines" the classes and all of the strategies from DI.
1. Find the strategy that matches the definition.
1. Get rid of large nasty switch statement.

The thing that "defines" the strategy can be very simple, like a name or enum, or something more complex to match on.

### Problems

- All strategies must be instatiated by the IoC container, which could be many or they could take a while to instantiate.

## 3. Dependency Injected Strategy (Lazy)

The original strategy with dependency injection caused the IoC container to instantiate each of the strategies which could be very slow and wasteful.

1. Remove the "definitions" from each of the strategy classes
1. In the IoC container registration for strategies, inject the class without the interface and now inject `Lazy<T,TMetadata>` where T is the Strategy and the metadata is the thing that "defines" the strategy.
   - ```csharp
     .AddScoped<ICoffeeStrategy, DripStrategy>()
     ```
     becomes
     ```csharp
     .AddScoped<DripStrategy>()
     .AddScoped(c=> new Lazy<ICoffeeStrategy, BrewMethod>(() => c.GetRequiredService<DripStrategy>(), BrewMethod.Drip))
     ```
1. In the context, instead of injecting all of the strategies, swap this for `IEnumerable<Lazy<T, TMetadata>>` and find the strategy based on the Metadata.

### Problems

- Probably not necessary since the constructors should be [simple](https://blog.ploeh.dk/2011/03/03/InjectionConstructorsshouldbesimple/)

## 4. Dependency Injected Strategy ()

Sometimes the thing that "defines" which strategy you are using is defined by something in configuration or somewhere else, e.g. feature flags. We can use the strategy pattern to implement [Branching By Abstraction](https://martinfowler.com/bliki/BranchByAbstraction.html).

For this we want to be able to swap out the strategies within the IoC without ever needing to implement a context to track the strategy. This means that the IoC container should serve up the correct strategy without the API controller calling `setStrategy`.

### Problems

- This only works if all of the information is accessible by the IoC container, e.g. other services or appsettings.

## References

- [dofactory - Strategy Design Pattern](https://www.dofactory.com/net/strategy-design-pattern)
- [Exception Not Found - The Strategy Design Pattern in C#](https://exceptionnotfound.net/strategy-the-daily-design-pattern/)
- [Refactoring Guru - Strategy](https://refactoring.guru/design-patterns/strategy)
- [Adam Storr - ASP.NET Core and the Strategy Pattern](https://adamstorr.azurewebsites.net/blog/aspnetcore-and-the-strategy-pattern)
