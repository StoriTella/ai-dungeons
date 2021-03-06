using System;
using smart_dungeons.Domain.Shared;
using Newtonsoft.Json;

namespace smart_dungeons.Domain.Users
{
    public class UserId: EntityId
    {
        public UserId(Guid value) : base(value)
        {
        }

        public UserId(String value) : base(value)
        {
        }

        override
        protected  Object createFromString(String text){
            return new Guid(text);
        }
 
        override
        public String AsString(){
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();
        }
        public Guid AsGuid(){
            return (Guid) base.ObjValue;
        }

    }
}