using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Interfaces;
using EaseTrail.WebApp.Models;
using EaseTrail.WebApp.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Services
{
    public class WorkSpaceContext : IWorkSpaceContext
    {
        private readonly Context _context;
        private readonly IUtilsContext _utilsContext;

        public WorkSpaceContext(
            Context context
            ,IUtilsContext utilsContext
        )
        {
            _utilsContext = utilsContext;
            _context = context;
        }

        public async Task<IActionResult> CreateWorkSpace(CreateWorkSpace input)
        {
            /*
             * Fazer verificação dos planos no futuro. Ex:
             *  Se o usuário tiver mais WorkSpaces do que o plano assinado, barrar a criação do WS.
             *  
             *  Atualmente (21/08) o sistema não possui uma tabela de planos nem uma tabela que faz a contagem organizada dos WS para Usuarios.
            */
            try
            {
                var user = _utilsContext.GetUserInfo();

                WorkSpace workSpace = new WorkSpace(new Guid(user.Id), input.Name, input.Description, input.Color, WorkSpaceStatus.Active);

                await _context.WorkSpaces.AddAsync(workSpace);
                await _context.SaveChangesAsync();

                return new CreatedResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
