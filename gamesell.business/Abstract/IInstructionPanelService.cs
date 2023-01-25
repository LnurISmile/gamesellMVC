using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IInstructionPanelService
    {
        InstructionPanel GetById(int id);
        List<InstructionPanel> GetAll(int page, int pageSize);
        List<InstructionPanel> GetAll();

        void Create(InstructionPanel entity);
        void Update(InstructionPanel entity);
        void Delete(InstructionPanel entity);
    }
}
