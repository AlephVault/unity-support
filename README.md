# Unity Support

This package contains core types and utilities for all the AlephVault-derived projects.

# Install

This package is not available in any UPM server. You must install it in your project like this:

1. In Unity, with your project open, open the Package Manager.
2. Either refer this Github project: https://github.com/AlephVault/unity-support.git or clone it locally and refer it from disk.
3. No more dependencies are required for this project.

# Usage

This section describes the features offered by this Support package.

## Behaviour classes

All these classes are accessible at namespace: `AlephVault.Unity.Support.Authoring.Behaviours`.

### `MonoBehaviourExtensions`

These are extensions to the `MonoBehaviour` class. Its purpose is to add some utilities to the `MonoBehaviour class`.

It adds the following members:

- `public void Invoke(() => DoSomething(...), someDelay)`: Invokes a given parameterless function after a given delay, expressed in float seconds. If `someDelay` is non-negative, invokes the target function (passed as first parameter) as a coroutine after the specified `someDelay` seconds. Otherwise, if negative, invokes it synchronously / immediately.

### `Normalized`

This is a behaviour that can be attached to any object. On `Awake()`, it forces the object's local position to (0, 0, 0), its rotation to the identity, and its scale to (1, 1, 1).

### `Throttler`

This is a behaviour that can be attached to any object. Its purpose is to allow throttled actions (i.e. actions that cannot be invoked THAT quickly but instead with a certain minimum interval or lapse before they can be allowed to be called). This may prevent malicius behaviours, mostly in multiplayer online games, for the game to not be saturated with a lot of requests.

This behaviour must first be configured for the object it's attached to. The property to care about is:

- `public float Lapse`: Expressed in seconds, tells the minimum amount of time to wait, after invoking a throttled call, before being allowed to invoke another throttled call. This must be setup to a positive number (negative numbers or zero will cause an immediate allowance, rendering thr throttler useless).
- `public Throttled(() => DoSomething(...))`: Invokes a given parameterless action under the throttle mechanism. This means that if this call was being done before the required `Lapse` time passed after a previous call, this call will do nothing. Otherwise, the passed action will be invoked synchronously.

### `AsyncQueueManager`

This is a behaviour that can be attached to any object. Its purpose is to provide a way to invoke and wait asynchronous code that is intended under these conditions:

1. The required function to invoke is Main-thread-only because interacts with game objects.
2. The current context wanting to launch the required function actually lies in another asynchronous context (e.g. a web connection or another form of Task with another synchronization context).

The motivation behind this class is that you cannot do [1] from a context in [2] directly, because the interaction with Unity objects is only allowed in the Main thread, so this class works as a bridge between both contexts.

It offers these methods (remember: you will typically invoke them in your asynchronous not-Main context):

- `public Task Queue(() => DoSomething(...))`: Queues a regular callback. Returns a task that resolves when the callback finishes (exceptions are forwarded).
- `public Task Queue(async () => DoSomething(...))`: Queues an async callback (a task-returning method). Returns a task that resolves when the callback finishes (exceptions are forwarded).
- `public Task<T> Queue<T>(async () => { DoSomething(...); return ...;})`: Same idea but this callback can return a value (the return value is also forwarded).

## Type classes

These are non-Object types or extensions with many purposes.

All these classes are accessible at namespace: `AlephVault.Unity.Support.Types`.

### `Async.AsyncOperationExtensionMethods`

These are extension to the `AsyncOperation` class. They make the `AsyncOperation` awaitable objects.

### `Sampling.RandomBox3`

This is a class that takes two vectors serving as bounding box and then generates random vectors inside that bounding box. Examples:

```
var box = new AlephVault.Unity.Support.Types.Sampling.RandomBox3(new Vector3(10, 15, 20), new Vector3(30, 40, 0));
// The bounding box will be properly created sorting x, y and z coordinates from those vectors.
Vector3 randomVector = box.Get();
// randomVector will be with x in [10..30], y in [15..40] and z in [0..20].
```

### `Exception`

This is just a base Exception class.

### `IdPool`

An IdPool is a way to retrieve unique `ulong` numbers each time. It's used in some classes to maintain connection ids or any source of cheap transient resource ids. This class is meant only for runtime purposes and never for persistent storage functions.

The object can be created in two ways:

```
// A pool with limit of 2^64 - 1.
var pool = new AlephVault.Unity.Support.Types.IdPool();

// A pool with a lower limit of 10000.
var pool2 = new AlephVault.Unity.Support.Types.IdPool(10000).
```

Once there, you can use one of those objects (e.g. `pool`) like this:

- `public ulong Next()`: Gets the next number, increasing the previous.
- `public void Dispose(ulong value)`: Disposes a value. This will be explained later.

Examples:

```
var a = pool.Next(); // a == 1
var b = pool.Next(); // b == 2
var c = pool.Next(); // c == 3.
```

You can also DISPOSE the ids. The disposed ids will be remembered and managed efficiently so they're forgotten when they're the greatest disposed values. The idea behind disposing a value is to tell "hey, this ID is not valid anymore. I'm returning it back to you". Depending on the disposed value, it may linger a long time among the disposed values or perhaps be forgotten and then generated in the next call to `Next()`. To dispose a value just call:

```
pool.Dispose(b); // 2 will be disposed.
pool.Dispose(c); // 3 will be disposed. Following this example, since 3 was the last generated id, will be immediately forgotten and will be available in the next call. Also, since 2 is now the last generated (non-forgotten) id and now disposed, 2 will also be forgotten and the last remembered id will be 1 (which is NOT disposed, in this example).
```

It's up to you to ensure that the disposed numbers are not used in your logic. After all, they're just regular numbers.

## Many utility classes

All these classes are located at namespace: `AlephVault.Unity.Support.Utils`.

### `Arithmetic`

It's a helper to deal with certain arithmetic functions.

It's a static class providing some methods:

```
long value = 0x1234567; // An arbitrary number.
ulong encoded = AlephVault.Unity.Support.Utils.Arithmetic.ZigZagEncode(value); // Encodes using ZigZag a ulong value. The sign bit is moved to position [0] and the rest of the number is displaced.
long decoded = AlephVault.Unity.Support.Utils.Arithmetic.ZigZagEncode(encoded); // Does the inverse process.

long value = 50000000; // An arbitrary number.
long result = AlephVault.Unity.Support.Utils.Arithmetic.Div8Ceil(value); // Divides by 8 and rounds up.

long value = 400000; // An arbitrary number
long size = AlephVault.Unity.Support.Utils.Arithmetic.VarIntSize(value); // Obtains the bytes size of a number. It uses a special encoding for packing the number.
```

### `Classes`

This is a helper class to deal with... classes.

It's a static class providing some methods:

- `public static bool IsSameOrSubclassOf(Type foo, Type bar)`: Tells whether type `foo` refers also type `bar`, or is a descendant of.
- `pubiic static bool IsSubclassOfRawGeneric(Type foo, Type bar)`: Tells whether type `foo` reders also type `bar`, or is a descendant of, or is a descendant of an application of a generic class (and this can be chained to many levels of checks of generics).
- `public static IEnumerable<Type> GetTypes()`: Gets all the types defined in the game.
- `public static IEnumerable<Type> GetTypes(param Assembly[] assemblies): Gets all the types defined in certain assembles in the game.
- `public static bool IsNullable(Type foo)`: Tells whether `foo` is a nullable type.
- `public static string FullyQualifiedProperty<T>(string property)`: Gets the full property name of a given property of class `T`, like `"Namespace.Of.My.Class.myProperty"`.

```
// Examples that check inheritance:
bool result = AlephVault.Unity.Support.Utils.Classes.IsSameOrSubclassOf(typeof(Foo), typeof(Bar)); // true iif foo is Bar or a descendant of Bar.
bool result = AlephVault.Unity.Support.Utils.Classes.IsSubclassOfRawGeneric(typeof(Foo), typeof(List<>)); // true iif foo is a subclass of a List<T>.

// Example to get the types defined in the whole game:
IEnumerable<Type> types = AlephVault.Unity.Support.Utils.Classes.GetTypes();
...
```

### `Tasks`

This is a helper class to deal with asynchronous calls.

It's a static class providing some methods:

- `public static async Task DefaultOnError(Exception e)`: A helper method used as default when dealing with asynchronous errors in certain utilities.
- `public static async Task Blink()`: A task that delays one frame when awaited.
- `public static async Task UntilDone(Task task[, Func<Exception, Task> onError = null])`: Waits for a task to finish and, if specified, captures the exception and processes it in a custom callback.
- `public static async Task UntilAllDone(IEnumerable tasks[, Func<Exception, Task> onError = null])`: Waits for MANY tasks to finish. Same idea than the previous method but for many tasks instead.

Examples:

```
// Waits one frame.
await AlephVault.Unity.Support.Tasks.Blink();

// Waits until a task is done and captures an error.
await AlephVault.Unity.Support.Tasks.UntilDone(someAsyncCall(), async (e) => Debug.LogException(e));
```

It also provides extension methods to `Func<..., Task>` methods, ranging from `Func<Task>` and `Func<T1, Task>` to `Func<T1, ..., T16, Task>` function pointers. Each version has its own call. For examples:

- `Func<Task> fnNoArgs = async () => ...; Task task = fnNoArgs.InvokeAsync(async (e) => ...handle error...)`: Invokes an async function with no arguments and waits for it to finish. A last argument being an async handler of an exception is OPTIONAL so it can be run if an exception is triggered to catch it.
- `Func<int, Task> fnNoArgs = async (x) => ...; Task task = fnNoArgs.InvokeAsync(1, async (e) => ...handle error...)`: Invokes an async function with one single argument `1` and waits for it to finish. A last argument being an async handler of an exception is OPTIONAL so it can be run if an exception is triggered to catch it.
- `Func<int, string, Task> fnNoArgs = async (x) => ...; Task task = fnNoArgs.InvokeAsync(1, "hello", async (e) => ...handle error...)`: Invokes an async function with an argument `1` and an argument `"hello"` and waits for it to finish. A last argument being an async handler of an exception is OPTIONAL so it can be run if an exception is triggered to catch it.
- ... and there are copies of InvokeAsync extending functions of UP TO 16 ARGUMENTS.

### `Values`

This is a helper class to deal with certain types of values.

It's a static class providing some methods:

- `public static T Min<T>(T a, T b)`: Compares any two comparable values and returns the minimum value of them.
- `public static T Max<T>(T a, T b)`: Compares any two comparable values and returns the maximum value of them.
- `public static T Clamp<T>(T? min, T value, T? max)`: Enforces a value to be between some given boundaries. Null boundaries are ignored, but otherwise the input value is modified to be not less than `min` and not greater than `max`.
- `public static bool In<T>(T? min, T value, T? max)`: Similar but only checks whether the value is in range, instead of modifying it.
- `public static Dictionary<K, V> Merge<K, V>(Dictionary<K, V> left, Dictionary<K, V> right, inPlace = true, (k, vLeft, vRight) => vLeft)`: Merges two dictionaries. If `inPlace` is true, then the first dictionary is affected in-place and also returned as value. Otherwise, then a new merged dictionary instance is generated. The fourth parameter is an OPTIONAL callback that resolves a new value when the two dictionaries have values on the same key. By default, when not specified, the criterion is to pick the value from the RIGHT/second dictionary, overriding what was specified in the LEFT/first dictionary.

Examples:

```
uint clamped = AlephVault.Unity.Support.Values.Clamp(1, 8, 7); // It will become 7.
bool isIn = AlephVault.Unity.Support.Values.In(1, 8, 7); // It will be false.
Dictionary<int, string> merged = AlephVault.Unity.Support.Values.Merge<int, string>(
    new Dictionary<int, string> {{1, "Foo"}, {2, "Bar"}},
    new Dictionary<int, string> {{1, "Foo2"}, {3, "Baz"}},
    false
); // It will return a dictionary like 1=>Foo2, 2=>Bar, 3=>Baz
```
