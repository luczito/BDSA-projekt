namespace GitInsight;
using GitInsight;
using LibGit2Sharp;

public class Lookup
{
    
    public void authorMode(string repopath)
    {
        List<User> userlist = new List<User>();
        var map = new Dictionary<string, User>();
        using (var repo = new Repository(repopath))

            ///Users/morten/Documents/GitHub/BDSA-projekt/GitInsight/Program.cs
        {

            var headCommit = repo.Head.Commits.ToList();
            foreach (var c in headCommit)
            {
                var user = new User(c.Author.Name);
                var name = c.Author.Name;

                User? uservalue;
                if (!map.TryGetValue(name, out uservalue))
                {
                    uservalue = new User(name);
                    map[name] = uservalue;
                }

                uservalue.commitlist.Add(
                    new GitInsight.Commit(c.Message, c.Author.When.Date));

            }

            foreach ((string name, User user) in map)
            {
                Console.WriteLine(user.userName);
                var histogram = from c in user.commitlist
                    group c by c.date
                    into h
                    let i = h.Count()
                    orderby h.Key
                    select new
                    {
                        Date = h.Key,
                        Count = i
                    };

                foreach (var commit in histogram)
                {
                    //Console.WriteLine(commit.Count + " " + commit.Date.ToString("yyyy-MM-dd"));
                    Console.WriteLine($"{commit.Count,6} {commit.Date:yyyy-MM-dd}");
                }

                Console.WriteLine("");
            }
        }

    }

    public void commitFrequency(string repopath)
    {
        using (var repo = new Repository(repopath))
        {
            var headCommit = repo.Head.Commits.ToList();

            var histogram = from c in headCommit
                group c by c.Author.When.Date
                into h
                let i = h.Count()
                orderby h.Key
                select new
                {
                    Date = h.Key,
                    Count = i
                };
            
            foreach (var commit in histogram)
            {
                //Console.WriteLine(commit.Count + " " + commit.Date.ToString("yyyy-MM-dd"));
                Console.WriteLine($"{commit.Count,6} {commit.Date:yyyy-MM-dd}");
            }

            Console.WriteLine("");
            
        }

        
    }
}