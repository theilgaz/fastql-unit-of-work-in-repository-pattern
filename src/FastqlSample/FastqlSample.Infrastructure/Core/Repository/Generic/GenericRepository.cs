using FastqlSample.Infrastructure.Models;
using Dapper;
using Fastsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FastqlSample.Infrastructure.Core.Repository
{
    public class GenericRepository<TEntity> : RepositoryBase, IGenericRepository<TEntity> where TEntity : class, new()
    {
        private FastqlBuilder<TEntity> fastql;

        public GenericRepository(IDbTransaction transaction) : base(transaction)
        { 
            fastql = new FastqlBuilder<TEntity>((TEntity)Activator.CreateInstance(typeof(TEntity)));
        }

        public Response<TEntity> Get(int id)
        {
            try
            {
                var returnData = Connection.Query<TEntity>(
                  $"SELECT * FROM {fastql.TableName()} WHERE Id=@Id",
                  param: new { Id = id },
                  transaction: Transaction
              ).FirstOrDefault();

                if (returnData != null)
                {
                    return new Response<TEntity>()
                    {
                        Entity = returnData,
                        IsSucceeded = true,
                        ResponseMessage = "Success"
                    };
                }
                else
                {
                    return new Response<TEntity>()
                    {
                        Entity = null,
                        IsSucceeded = true,
                        ResponseMessage = "Not found"
                    };

                }
            }
            catch (System.Exception ex)
            {
                return new Response<TEntity>()
                {
                    Entity = null,
                    IsSucceeded = false,
                    ResponseMessage = "Error: " + ex.Message
                };
            }

        }

        public Response<TEntity> Get(string where)
        {
            try
            {
                var returnData = Connection.Query<TEntity>(
                $"SELECT * FROM {fastql.TableName()} WHERE {where}",
                    //param: id,
                    transaction: Transaction
                  ).FirstOrDefault();

                if (returnData != null)
                {
                    return new Response<TEntity>()
                    {
                        Entity = returnData,
                        IsSucceeded = true,
                        ResponseMessage = "Success"
                    };
                }
                else
                {
                    return new Response<TEntity>()
                    {
                        Entity = null,
                        IsSucceeded = true,
                        ResponseMessage = "Not found"
                    };

                }
            }
            catch (System.Exception ex)
            {
                return new Response<TEntity>()
                {
                    Entity = null,
                    IsSucceeded = false,
                    ResponseMessage = "Error: " + ex.Message
                };
            }
        }

        public Response<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                var returnData = Connection.Query<TEntity>(
                $"SELECT * FROM {fastql.TableName()}",
                transaction: Transaction
                );

                if (returnData != null)
                {
                    return new Response<IEnumerable<TEntity>>()
                    {
                        Entity = returnData,
                        IsSucceeded = true,
                        ResponseMessage = "Success"
                    };
                }
                else
                {
                    return new Response<IEnumerable<TEntity>>()
                    {
                        Entity = null,
                        IsSucceeded = true,
                        ResponseMessage = "Not found"
                    };

                }
            }
            catch (System.Exception ex)
            {

                return new Response<IEnumerable<TEntity>>()
                {
                    Entity = null,
                    IsSucceeded = false,
                    ResponseMessage = "Error: " + ex.Message
                };
            }
        }

        public Response<IEnumerable<TEntity>> GetAll(string where)
        {
            try
            {
                var returnData = Connection.Query<TEntity>(
                $"SELECT * FROM {fastql.TableName()} WHERE {where}",
                transaction: Transaction
                );

                if (returnData != null)
                {
                    return new Response<IEnumerable<TEntity>>()
                    {
                        Entity = returnData,
                        IsSucceeded = true,
                        ResponseMessage = "Success"
                    };
                }
                else
                {
                    return new Response<IEnumerable<TEntity>>()
                    {
                        Entity = null,
                        IsSucceeded = true,
                        ResponseMessage = "Not found"
                    };

                }
            }
            catch (System.Exception ex)
            {

                return new Response<IEnumerable<TEntity>>()
                {
                    Entity = null,
                    IsSucceeded = false,
                    ResponseMessage = "Error: " + ex.Message
                };
            }
        }

        public Response<long> Insert(TEntity entity)
        {
            try
            {
                var result = Connection.Execute(
                fastql.InsertQuery(),
               param: entity,
               transaction: Transaction
               );

                if (result == 1)
                {
                    return new Response<long>()
                    {
                        Entity = result,
                        IsSucceeded = true,
                        ResponseMessage = "Success"
                    };
                }
                else
                {
                    return new Response<long>()
                    {
                        Entity = result,
                        IsSucceeded = false,
                        ResponseMessage = "Not inserted"
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new Response<long>()
                {
                    Entity = 0,
                    IsSucceeded = false,
                    ResponseMessage = "Error: " + ex.Message
                };
            }

        }
         
        public Response<bool> Update(TEntity entity, string where)
        {
            try
            {
                var result = Connection.Execute(
                  fastql.UpdateQuery(entity, where),
                  param: entity,
                  transaction: Transaction
              );

                if (result > 0)
                {
                    return new Response<bool>()
                    {
                        Entity = true,
                        IsSucceeded = true,
                        ResponseMessage = "Success"
                    };
                }
                else
                {
                    return new Response<bool>()
                    {
                        Entity = false,
                        IsSucceeded = false,
                        ResponseMessage = "Not updated"
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new Response<bool>()
                {
                    Entity = false,
                    IsSucceeded = false,
                    ResponseMessage = "Error: " + ex.Message
                };
            }
        }

        public Response<bool> Delete(string where)
        {
            try
            {
                var result = Connection.Execute(
                  $"DELETE FROM {fastql.TableName()} WHERE {where}",
                  // param: entity,
                  transaction: Transaction
              );
                if (result > 0)
                {
                    return new Response<bool>()
                    {
                        Entity = true,
                        IsSucceeded = true,
                        ResponseMessage = "Success"
                    };
                }
                else
                {
                    return new Response<bool>()
                    {
                        Entity = false,
                        IsSucceeded = false,
                        ResponseMessage = "Not updated"
                    };
                }
            }
            catch (System.Exception ex)
            {

                return new Response<bool>()
                {
                    Entity = false,
                    IsSucceeded = false,
                    ResponseMessage = "Error: " + ex.Message
                };
            }
        }

        public Response<int> Count(string where = null)
        {
            try
            {
                string _where = (where == null) ? ";" : $" WHERE {where}";
                var returnData = Connection.Query<int>(
                  $"SELECT COUNT(1) FROM {fastql.TableName()}{_where}",
                  transaction: Transaction
              ).FirstOrDefault();

                if (returnData > 0)
                {
                    return new Response<int>()
                    {
                        Entity = returnData,
                        IsSucceeded = true,
                        ResponseMessage = "Success"
                    };
                }
                else
                {
                    return new Response<int>()
                    {
                        Entity = 0,
                        IsSucceeded = true,
                        ResponseMessage = "Not found"
                    };

                }
            }
            catch (System.Exception ex)
            {
                return new Response<int>()
                {
                    Entity = -1,
                    IsSucceeded = false,
                    ResponseMessage = "Error: " + ex.Message
                };
            }
        }
    }
}
