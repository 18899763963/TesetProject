using SmallManagerSpace.Resources.DataBase.NewFolder1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallManagerSpace.Resources.DataBase.BookTest
{
    public class TestBook
    {
        public void btnAdd_Click()
        {
            List<Book> books = new List<Book>()
            {
                new Book() { Name = "射雕英雄传", PublishDate = new DateTime(1960, 1, 1), Author = "金庸", Price = 10.5f },
                new Book() { Name = "神雕侠侣", PublishDate = new DateTime(1960, 2, 2), Author = "金庸", Price = 12.5f },
                new Book() { Name = "倚天屠龙记", PublishDate = new DateTime(1960, 3, 3), Author = "金庸", Price = 16.5f },
                new Book() { Name = "小李飞刀", PublishDate = new DateTime(1965, 5, 5), Author = "古龙", Price = 13.5f },
                new Book() { Name = "绝代双骄", PublishDate = new DateTime(1965, 6, 6), Author = "古龙", Price = 15.5f },
            };

            using (var db = new MyBookDB())
            {
                db.Books.AddRange(books);
                int count = db.SaveChanges();
                string Text = $"{DateTime.Now}, 插入{count}条记录";
                Console.WriteLine(Text);
            }
        }

        public void btnModify_Click()
        {
            using (var db = new MyBookDB())
            {
                var book = db.Books.FirstOrDefault(x => x.Name == "绝代双骄");
                if (book != null)
                {
                    book.Price += 1;
                    int count = db.SaveChanges();
                    string Text = $"{DateTime.Now}, 修改{count}条记录";
                    Console.WriteLine(Text);
                }
            }
        }

        public void btnDel_Click()
        {
            using (var db = new MyBookDB())
            {
                var book = db.Books.FirstOrDefault(x => x.Name == "绝代双骄");
                if (book != null)
                {
                    var result = db.Books.Remove(book);
                    int count = db.SaveChanges();
                    string Text = $"{DateTime.Now}, 删除{count}条记录";
                    Console.WriteLine(Text);
                }
            }
        }

        public void btnQuery_Click()
        {
            using (var db = new MyBookDB())
            {
                var books = db.Books.Where(x => x.Author == "金庸").OrderByDescending(x => x.PublishDate).ToList();
                string Text = $"{DateTime.Now}, 查到{books.Count}条记录";
                //this.dataGridView1.DataSource = books;
                Console.WriteLine(Text);
            }
        }

        public void btnRefresh_Click()
        {
            using (var db = new MyBookDB())
            {
                var books = db.Books.ToList();
                //this.dataGridView1.DataSource = books;
                //Console.WriteLine(Text);
            }
        }
    }
}
