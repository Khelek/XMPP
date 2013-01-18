using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using agsXMPP;

namespace serializ2
{
[Serializable]
    partial class OneConferention
    {
        public string id;
        public string name;
        public List<string> users;
        public int length
        {
            get { return users.Count; }
        }

        public OneConferention()
        {
            id = null;
            name = null; ;
            users = new List<string>();
        }

        public OneConferention(string _id, string _name, string creator) 
        {
            id = _id;
            name = _name;
            users = new List<string>();
            users.Add(creator);
        }

        public OneConferention(string _id, string _name, List<string> _users)
        {
            id = _id;
            name = _name;
            users = _users;
        }

        public OneConferention(string _id, string _name, string creator, List<string> _users)
        {
            id = _id;
            name = _name;
            users = _users;
            users.Add(creator);
        }

        public void AddUser(string user)
        {
            if(!users.Contains(user))
                users.Add(user);
        }

        public void DelUser(string user)
        {
            users.Remove(user);
        }

        public string GetUser(int index)
        {
            return users[index];
        }

        public int IndexOf(string user)
        {
            return users.IndexOf(user);
        }

        public string toString()
        {
            string str = id + "/" + name + "/" + users.Count;
            for (int i = 0; i < users.Count; i++)
            {
                str += "/" + users[i];
            }
            return str;
        }

        public void historyToFile(string filepath, string history)
        {
            using (StreamWriter writer = File.AppendText(filepath + "\\conferentions.txt"))
            {
                writer.WriteLine(history + "\n");
            }
        }
    }

    class Conferentions
    {
        private List<OneConferention> conferentions;
        public int length
        {
            get { return conferentions.Count; }
        }

        public Conferentions()
        {
            conferentions = new List<OneConferention>();
        }

        public OneConferention this[int x]
        {
            get
            {
                if (x < conferentions.Count)
                {
                    return conferentions[x];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                conferentions[x] = value;
            }
        }

        public void Add(OneConferention conf)
        {            
            conferentions.Add(conf);
        }

        public void Remove(OneConferention conf)
        {
            conferentions.Remove(conf);
        }

        public bool Contains(string id)
        {
            return Find(id) == -1 ? false : true;
        }


        public int Find(string id)
        {
            for (int i = 0; i < length; i++)
            {
                if (conferentions[i].id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public void toFile(string filepath)
        {
            filepath = filepath.Trim();
            for (int i = 0; i < conferentions.Count; i++)
            {
                using (StreamWriter writer = File.AppendText(filepath + "\\conferentions.txt"))
                {
                    writer.WriteLine(conferentions[i].toString() + "\n");
                }
            }
        }



    }
}
