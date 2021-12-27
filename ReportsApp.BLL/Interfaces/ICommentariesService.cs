using System;
using System.Collections.Generic;
using ReportsApp.DAL.Entities;

namespace ReportsApp.BLL.Interfaces
{
    public interface ICommentariesService
    {
        List<Commentary> GetAll();
        
        Commentary GetById(Guid id);

        List<Commentary> GetCommentsByUser(Guid id);

        List<Commentary> GetCommentsByTask(Guid id);

        void DeleteCommentary(Guid id);

        void CreateCommentary(Commentary commentary);
    }
}