namespace SignalRChat.Models
{
    public class ConUserModel
    {
        public string connectID { get; set; }
        public string username { get; set; }
        public DateTime onlineTime { get; set; }
    }

    public class ConUserService
    {
        private List<ConUserModel> _list;
        public ConUserService()
        {
            _list = new List<ConUserModel>();
        }
        public List<ConUserModel> AddList(ConUserModel user)
        {
            _list.Add(user);
            return _list;
        }
        public List<ConUserModel> RemoveList(string connectID)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].connectID == connectID)
                    _list.Remove(_list[i]);
            }
            return _list;
        }
    }
}
