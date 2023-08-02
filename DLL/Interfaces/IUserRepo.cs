﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    internal interface IUserRepo<MODELCLASS,NUMBER,LOGIC,OTHERS>
    {
        List<MODELCLASS> GetAll();
        MODELCLASS GetByID(NUMBER id);
        MODELCLASS GetByName(OTHERS name);
        LOGIC Create(MODELCLASS obj); 
        LOGIC Delete(NUMBER id);
        LOGIC Update(MODELCLASS obj);
    }
}
