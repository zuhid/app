namespace Zuhid.BaseApi;

public interface IValidator<TModel> {
  List<(string, string)> Validate(TModel model);
}
