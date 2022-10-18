namespace Zuhid.ApiBase;

public interface IValidator<TModel> {
  List<(string, string)> Validate(TModel model);
}
