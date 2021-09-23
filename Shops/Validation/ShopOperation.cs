using System;
using System.Collections.Generic;
using Shops.Models;

namespace Shops.Validation
{
    public class ShopOperation : Operation<Shop>
    {
        public ShopOperation(List<object> allowedActors, Action<Shop, dynamic[]> action)
            : base(allowedActors, action)
        {
        }

        protected override bool Authorize(object actor) => _allowedActors.Contains(actor.GetType());
    }
}