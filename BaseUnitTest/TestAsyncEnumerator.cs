namespace Zuhid.BaseUnitTest;

public class TestAsyncEnumerator<T> : IAsyncEnumerator<T> {
  private readonly IEnumerator<T> inner;

  public TestAsyncEnumerator(IEnumerator<T> inner) => this.inner = inner;

  public T Current => inner.Current;

  public ValueTask DisposeAsync() {
    inner.Dispose();
    return new ValueTask();
  }

  public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(inner.MoveNext());
}
