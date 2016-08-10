using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DBRep
    {
        public Användare getUser(int ID)
        {
                using (var context = new BortaMatchDBEntities())
                {
                    var returvärde = context.Användare.Where(x => x.UserID == ID).FirstOrDefault() as Användare;
                    return returvärde;
                }
        }

        public bool korrektID(int id)
        {
            bool returvärdet = false;

            using(var context = new BortaMatchDBEntities())
            {
                var tempLista = context.Användare.ToList();
                foreach(var item in tempLista)
                {
                    if (item.UserID == id)
                    {
                        returvärdet = true;
                        return returvärdet;
                    }
                }
            }

            return returvärdet;
        }

        public Användare getUserFromString(string UName)
        {
            using (var context = new BortaMatchDBEntities())
            {
                var returnvalue = context.Användare.Where(u => u.UName == UName).FirstOrDefault();
                return returnvalue;
            }
        }

        public bool ledigtAnvNamn(string anvNamn)
        {
            bool returvärdet = true;

            using(var context = new BortaMatchDBEntities())
            {
                var tempListan = context.Användare.ToList();

                foreach(var item in tempListan)
                {
                    if (item.UName.Equals(anvNamn))
                    {
                        returvärdet = false;
                    }
                }
            }

            return returvärdet;
        }

        public bool ledigEmail(string email)
        {
            bool returvärdet = true;
            using(var context = new BortaMatchDBEntities())
            {
                foreach(var item in context.Användare)
                {
                    if (item.Email.Equals(email))
                    {
                        returvärdet = false;
                    }
                }
            }

            return returvärdet;
        }

        public List<Användare> getRandomUsers()
        {
            using (var context = new BortaMatchDBEntities())
            {

                var temp = context.Användare.ToList();

                List<Användare> Listan = new List<Användare>();
                Random rnd = new Random();

                for (int i = 0; i < 3; i++)
                {
                    int temp2 = rnd.Next(0, temp.Count);
                    var AnvTemp = temp[temp2];
                    Listan.Add(AnvTemp);
                }

                return Listan;
            }
        }

        public List<ProfilInfo> getRandomFriendProfiles(int id)
        {
            using (var context = new BortaMatchDBEntities())
            {
                var tempfriendsList = getFriends(id);

                List<ProfilInfo> Listan = new List<ProfilInfo>();
                List<int> usedListan = new List<int>();
                Random rnd = new Random();

                for (int i = 0; i < 3; i++)
                {
                    if (tempfriendsList.Count != 0)
                    {
                        var tempInt = 0;
                        if (tempfriendsList.Count == 1)
                        {
                            tempInt = 0;
                        }
                        else
                        {
                            tempInt = rnd.Next(1, tempfriendsList.Count - 1);
                        }
                        if (usedListan.Contains(tempInt))
                        {
                            continue;
                        }
                        if (tempfriendsList[tempInt].FirstUID == id)
                        {
                            Listan.Add(getProfile(tempfriendsList[tempInt].SecondUID));
                            usedListan.Add(tempInt);
                        }
                        else
                        {
                            Listan.Add(getProfile(tempfriendsList[tempInt].FirstUID));
                            usedListan.Add(tempInt);
                        }
                    }
                }

                return Listan;
            }
        }

        public ProfilInfo getProfile(int ID)
        {
            using (var context = new BortaMatchDBEntities())
            {
                var Profil = context.ProfilInfo.Where(x => x.UID == ID).FirstOrDefault() as ProfilInfo;
                return Profil;
            }
        }

        public List<FriendRequest> getFriendRequests(int ID)
        {
            List<FriendRequest> Listan = new List<FriendRequest>();

            using (var context = new BortaMatchDBEntities())
            {
                foreach (var item in context.FriendRequest)
                {
                    if (item.MUID == ID)
                    {
                        Listan.Add(item);
                    }
                }
            }

            return Listan;
        }

        public void NewUser(Användare NewAnv)
        {
            using (var context = new BortaMatchDBEntities())
            {
                context.Användare.Add(NewAnv);
                context.SaveChanges();
            }

            using (var context = new BortaMatchDBEntities())
            {
                ProfilInfo tempProfil = new ProfilInfo();
                tempProfil.URL = "http://www.volontarprojektet.se/wp-content/uploads/2015/04/lamaselfie.png";
                var temp = context.Användare.Where(x => x.UName == NewAnv.UName).FirstOrDefault();
                tempProfil.UID = temp.UserID;
                tempProfil.ENamn = "Doe";
                tempProfil.FNamn = "John";
                tempProfil.Intresse = "Lorium Ipsum";
                tempProfil.Kön = 0;
                tempProfil.Ålder = 100;

                context.ProfilInfo.Add(tempProfil);
                context.SaveChanges();
            }

        }

        public List<Användare> SearchResult(string search)
        {
            using (var context = new BortaMatchDBEntities())
            {
                List<Användare> Listan = context.Användare.Where(x => x.UName.Contains(search)).ToList();
                return Listan;
            }
        }

        public List<Friend> getFriends(int id)
        {
            using (var context = new BortaMatchDBEntities())
            {
                List<Friend> tempListan = context.Friend.Where(x => x.FirstUID == id).ToList();
                List<Friend> tempListan2 = context.Friend.Where(x => x.SecondUID == id).ToList();

                foreach (var item in tempListan2)
                {
                    tempListan.Add(item);
                }

                return tempListan;
            }
        }

        public List<FriendRequest> getAllFriendRequests(int id)
        {
            using (var context = new BortaMatchDBEntities())
            {
                List<FriendRequest> templistan = context.FriendRequest.Where(x => x.MUID == id).ToList();
                List<FriendRequest> templistan2 = context.FriendRequest.Where(x => x.SUID == id).ToList();

                foreach (var item in templistan2)
                {
                    templistan.Add(item);
                }

                return templistan;
            }
        }

        public void newFriendRequest(FriendRequest newFR)
        {
            using (var context = new BortaMatchDBEntities())
            {
                context.FriendRequest.Add(newFR);
                context.SaveChanges();
            }
        }

        public void deleteFriendRequest(int SID, int MID)
        {
            using (var context = new BortaMatchDBEntities())
            {
                var temp = context.FriendRequest.Where(x => x.MUID == MID && x.SUID == SID).FirstOrDefault();
                context.FriendRequest.Remove(temp);
                context.SaveChanges();
            }
        }

        public void newFriend(Friend newFriend)
        {
            using (var context = new BortaMatchDBEntities())
            {
                context.Friend.Add(newFriend);
                context.SaveChanges();
            }
        }

        public List<Wall> getInlägg(int id)
        {
            using (var context = new BortaMatchDBEntities())
            {
                List<Wall> temp = context.Wall.Where(x => x.UID == id).ToList();
                return temp;
            }
        }

        public void taBortInlägg(int wid)
        {
            using (var context = new BortaMatchDBEntities())
            {
                var temp = context.Wall.Where(x => x.WID == wid).FirstOrDefault();
                context.Wall.Remove(temp);
                context.SaveChanges();
            }
        }

        public void läggTillPost(int sender, int receiver, string post)
        {
            var temp = new Wall();
            temp.SID = sender;
            temp.UID = receiver;
            temp.Post = post;

            using (var context = new BortaMatchDBEntities())
            {
                context.Wall.Add(temp);
                context.SaveChanges();
            }
        }

        public void uppdateraAnvändaren(Användare UppdateradeUsern, ProfilInfo UppdateradeProfilen)
        {
            using (var context = new BortaMatchDBEntities())
            {
                var e = context.Användare.Where(y => y.UserID == UppdateradeUsern.UserID).FirstOrDefault();
                e.Email = UppdateradeUsern.Email;
                e.PW = UppdateradeUsern.PW;
                e.Sökbar = UppdateradeUsern.Sökbar;
                e.UName = UppdateradeUsern.UName;

                var x = context.ProfilInfo.Where(m => m.PID == UppdateradeProfilen.PID).FirstOrDefault();
                x.ENamn = UppdateradeProfilen.ENamn;
                x.FNamn = UppdateradeProfilen.FNamn;
                x.Intresse = UppdateradeProfilen.Intresse;
                x.Kön = UppdateradeProfilen.Kön;
                x.URL = UppdateradeProfilen.URL;
                x.Ålder = UppdateradeProfilen.Ålder;
                context.SaveChanges();
            }
        }
    }
}
