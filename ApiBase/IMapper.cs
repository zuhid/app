namespace Zuhid.ApiBase;

public interface IMapper<TEntity, TModel> {
  TEntity GetEntity(TModel model);
}
