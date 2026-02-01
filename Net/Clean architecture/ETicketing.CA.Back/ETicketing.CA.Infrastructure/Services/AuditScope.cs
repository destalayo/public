using Common.Utils.Tools.Models;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Models;

namespace ETicketing.CA.Infrastructure.Services
{
    public class AuditScope:IAuditScope
    {
        public AuditDTO Audit { get; set; }     
         public void AddChanges(List<AuditEntityDTO> changes)
        {
            Audit.Entities.AddRange(changes);
        }
        public void AddChild(AuditDTO child) {
            Audit.Children.Add(child);
        }
        public async Task<T> RegisterAudit<T>(string userId, IAuditScope? parent, Func<Task<T>> p)
        {
            return await BaseTryCatch(userId, parent, async () => await p());
        }

        public async Task RegisterAudit(string userId, IAuditScope? parent, Func<Task> p)
        {
            await BaseTryCatch(userId, parent, async () =>
            {
                await p();
                return true;
            });
        }


        private async Task<T> BaseTryCatch<T>(string userId, IAuditScope? parent,Func<Task<T>> action)
        {
            try
            {
                Audit=new AuditDTO();
                Audit.UserId = userId;
                Audit.StartDateTime = DateTime.UtcNow;
                return await action();
            }
            catch (BusinessException e)
            {
                Audit.HasError = true;
                Audit.Log = e.Message;
                if (parent == null)
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                Audit.HasError = true;
                Audit.Log = $"Error no controlado: {string.Join(Environment.NewLine, CalculateAllInnerMessages(e))}";
                if (parent == null)
                {
                    throw;
                }
            }
            finally
            {
                Audit.EndDateTime = DateTime.UtcNow;

                if (parent != null)
                {
                    parent.AddChild(Audit);
                }
            }
            return default(T);
        }
        public List<string> CalculateAllInnerMessages(Exception e)
        {
            List<string> result = new List<string>();
            CalculateMsgException(ref result, e);
            return result;
        }
        private void CalculateMsgException(ref List<string> msgs, Exception e)
        {
            msgs.Add(e.Message);
            if (e.InnerException != null)
            {
                CalculateMsgException(ref msgs, e.InnerException);
            }
        }
    }
}
