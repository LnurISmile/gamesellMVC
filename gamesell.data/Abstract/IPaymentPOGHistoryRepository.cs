﻿using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IPaymentPOGHistoryRepository : IRepository<PaymentPOGHistory>
    {
        int GetCount();
        PaymentPOGHistory GetAdDetails(int id);
    }
}
