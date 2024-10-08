﻿using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Interfaces;
using EaseTrail.WebApp.Models;
using EaseTrail.WebApp.Models.Enums;
using EaseTrail.WebApp.Outputs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<WorkSpace>> GetAll()
        {
            try
            {
                return await _context.WorkSpaces.ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<WorkSpace> GetById(Guid workSpaceId)
        {
            try
            {
                var workSpace = await _context.WorkSpaces.FindAsync(workSpaceId);

                return workSpace;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<List<WorkSpace>> GetByUserId(string id = "")
        {
            string getId = "";

            if (string.IsNullOrEmpty(id))
            {
                getId = _utilsContext.GetUserInfo().Id;
            }
            else
            {
                getId = id;
            }

            var workSpaces = await _context.WorkSpaces.Where(x => x.OwnerId == new Guid(getId)).ToListAsync();

            if (workSpaces == null)
            {
                throw new Exception("Não foi possivel encontrar nenhum workspace");
            }

            return workSpaces;
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

        public async Task<IActionResult> UpdateWorkSpace(UpdateWorkSpace input, Guid workSpaceId)
        {
            try
            {
                var user = _utilsContext.GetUserInfo();

                var workSpace = await _context.WorkSpaces.FirstOrDefaultAsync(x => x.OwnerId == new Guid(user.Id) && x.Id == workSpaceId);

                if (workSpace == null)
                {
                    throw new Exception("Workspace não encontrado");
                }

                UpdateWorkSpace(workSpace, input);

                await _context.SaveChangesAsync();

                return new CreatedResult();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IActionResult> DeleteWorkSpace(Guid workSpaceId)
        {
            try
            {
                var user = _utilsContext.GetUserInfo();

                var workSpace = await _context.WorkSpaces.FirstOrDefaultAsync(x => x.OwnerId == new Guid(user.Id) && x.Id == workSpaceId);

                if (workSpace == null)
                {
                    throw new Exception("WorkSpace não encontrado");
                }

                _context.WorkSpaces.Remove(workSpace);

                await _context.SaveChangesAsync();

                return new OkObjectResult("WorkSpace deletado com sucesso");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        #region private methods

        private void UpdateWorkSpace(WorkSpace workSpace, UpdateWorkSpace input)
        {
            if (!string.IsNullOrEmpty(input.Name) && input.Name != workSpace.Name)
                workSpace.Name = input.Name;

            if (!string.IsNullOrEmpty(input.Description) && input.Description != workSpace.Description)
                workSpace.Description = input.Description;

            if (!string.IsNullOrEmpty(input.Color) && input.Color != workSpace.Color)
                workSpace.Color = input.Color;

            if (input.Status > 0 && (WorkSpaceStatus)input.Status != workSpace.Status)
                workSpace.Status = (WorkSpaceStatus)input.Status;
        }

        #endregion
    }
}
