# Clean CQRS

Clean CQRS is a library to set up code abstractions that runs commands and queries against a unit of work.

It is minimal library to promote a predictable and testable code base.

## Concepts

Without delving into more details about CQRS and Units Of Work, for which there are numerous resources, CleanCQRS breaks concepts down to

### 1. Unit of Work

A unit of work controls the isolation scope of the work, for example a transaction. Everything is run against a unit of work.

It can be used to compose dependencies, and can also be extended to support more complex concepts.

### 2. Commands

A command performs an operation and can return a result.

Command handlers can call on to other commands and queries to allow composable reuse.

Command handlers could also inherit from base handlers as an alternative way to reuse code.

### 3. Queries

A query returns data.

Similarly to command handlers, query handlers can also inherit or call other queries.

### 4. Pipeline

An pipeline can optionally be used to wrap every command and query run. You can chose to setup a pipeline or run without one.

## Download & Install

### NuGet
```powershell
Install-Package CleanCQRS
```
### Command Line
```powershell
dotnet add package CleanCQRS
```

## Setup

There are multiple of ways to set up your project but the basics are

### 1. Define a unit of work interface
 - This interface has Run methods on it to invoke a query or command
 - There are base interfaces to inherit from eg `IUnitOfWorkCommand` or `IUnitOfWorkCommandWithoutCancellationToken`
 - Add any other dependencies you chose that might be common to many queries and command eg a clock interface `IClock` to abstract away `System.DateTime` from your testing.

### 2. A command must inherit from the `ICommand<T>` or `ICommand` interface.

### 3. A query must inherit from the `IQuery<T>` interface.

### 4. Every Command or Query has a single handler 
 - Its usually (depending on your style preferences) a good idea to nest them within the Command or Query definition class.

 - This handler must implement `IRequestHandler<TUnitOfWork, TCommand, TResult>` however base classes are provided to make it easier eg `AsyncCommandHandlerBase<TUnitOfWork, TCommand, TResult>`
 
 - It is often a good idea to have your own base class in your central/core project to tie down the Unit of Work interface used, but its up to you.

 - The constructor of a handler can take any necessary dependencies that aren't exposed by the unit of work.

- Handlers are automatically registered with Dependency Injection

### 5. Decide if you want a pipeline. 

 - If so inherit it from `IPipeline<TUnitOfWork, TRequest, TResponse>`
 
 - Pipelines are great for logging/exceptions etc, but the same can be achieved with base handler classes, so its a question of preference.
 
 - By design a single pipeline is supported as you can separate concerns by calling any child dependencies or methods and a single pipeline removes the possibility of ordering complications etc.
 
 - Avoid adding logic to pipelines but you have full access to the command / query / handler and unit of work
 
 - Pipelines need registering with Dependency Injection

### 6. Implement your Unit of Work interface

- Take a dependency on `ICQRSRequestHandler` and call this to run commands or queries 
 
			public Task Run(ICommand command, CancellationToken cancellationToken) 
				=> _requestHandler.HandleCommand<IUnitOfWork>(this, command, cancellationToken);`

    		public Task<T> Run<T>(ICommand<T> command, CancellationToken cancellationToken) 
				=> _requestHandler.HandleCommand<IUnitOfWork, T>(this, command, cancellationToken);`
    
    		public Task<T> Run<T>(IQuery<T> query, CancellationToken cancellationToken) 
				=> _requestHandler.HandleQuery<IUnitOfWork, T>(this, query, cancellationToken);
 
- Its often good to add `IDisposable` to support `using(var uow = UnitOfWork()){}`

### 7. Decide how you are going to construct a unit of work

- Its often nice to define a `IUnitOfWorkProvider` that construct your unit of work so that it very explicit where new units or work are started.
 
			public interface IUnitOfWorkProvider
			{
				IUnitOfWork Start();
			}

- You can also just depend on a `Func<IUnitOfWork>()`

### 8. Register with dependency injection passing in the assembly that holds all your commands and queries

- Uses Microsoft DI by default
	    	
		services.AddCleanCQRS(Assembly);

- Add your UnitOfWork and any other dependencies

		services.AddTransient<IUnitOfWorkProvider, UnitOfWorkProvider>();
		// or
		services.AddTransient<Func<IUnitOfWork>>(provider => new UnitOfWork(provider));

- Although its possible, registering IUnitOfWork with Dependency Injection is often best avoided as you want to control how Units Of Work are constructed and make it explicit

### There are some examples and how to test them included here.

Examples are all a bit contrived and simplistic but hopefully convey the power of this set up for testing and being able to change decisions in the future...

1. Minimal
	- Console app that constructs a unit of work and defines commands and queries with no base classes.
	- [/Examples/MinimalSetup/MinimalSetup/Program.cs](/Examples/MinimalSetup/MinimalSetup/Program.cs)
2. Simple
	- Project setup using Onion Architecture that constructs a unit of work from a provider and defines commands and queries with base classes.
	- Web project as example entry point
	- [/Examples/SimpleSetup/SimpleSetup.Web/Program.cs](/Examples/SimpleSetup/SimpleSetup.Web/Program.cs)
3. Pipeline
	- Similar to Simple but with a pipeline added
    - [/Examples/PiplineSetup/PiplineSetup.Web/Program.cs](/Examples/PiplineSetup/PiplineSetup.Web/Program.cs)
3. Composition
	- The Unit Of Work is composed of all dependencies
    - [/Examples/ComposedSetup/ComposedSetup.Web/Program.cs](/Examples/ComposedSetup/ComposedSetup.Web/Program.cs)
4. Isolated
	- Commands and Queries run against different interfaces meaning strict separation is type checked. The pipeline also behaves differently for commands and queries.
    - [/Examples/IsolatedSetup/IsolatedSetup.Web/Program.cs](/Examples/IsolatedSetup/IsolatedSetup.Web/Program.cs)
5. Barebones
	- Absolute minimal setup without unit of work but still uses Commands and Queries
    - [/Examples/BarebonesSetup/Barebones.Console/Program.cs](/Examples/BarebonesSetup/Barebones.Console/Program.cs)
6. Synchronous
	- Minimal setup only using sync methods as store is guarantied to be synchronous
    - [/Examples/SyncSetup/Sync.Console/Program.cs](/Examples/SyncSetup/Sync.Console/Program.cs)

