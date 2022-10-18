namespace Zuhid.BaseApi;

public interface IMapper<TEntity, TModel> {
  TEntity GetEntity(TModel model);
}
