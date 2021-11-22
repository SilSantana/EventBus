using MediatR;

namespace Demo.EventBus.Service.Models.Message
{
    public class MessageRequest : MessageHandlerModel, INotification
    {
        public override string ToString()
        {
            return "{ Id :" + Id + " Description: " + Descripetion +  " }";
        }

    }
}
