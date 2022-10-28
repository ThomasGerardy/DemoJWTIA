using Demo_ASP_MVC_06_Session.DAL.Interfaces;
using Demo_ASP_MVC_06_Session.DAL.Utils;
using Demo_ASP_MVC_06_Session.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_06_Session.DAL.Repositories
{
    public class MessageRepository : RepositoryBase<int, Message>, IMessageRepository
    {
        public MessageRepository(IDbConnection connection) : base(connection)
        { }

        public override int Add(Message entity)
        {
            IDbCommand cmd = _connection.CreateQueryCommand("INSERT INTO [Message](Content, Create_At, Member_Id OUPUT [inserted].[Member_Id] VALUES (@Content, @CreateAt, @MemberId)");

            cmd.AddParam("@Content", entity.Content);
            cmd.AddParam("@CreateAt", entity.CreateAt);
            cmd.AddParam("@MemberId", entity.CreateAt);

            _connection.Open();

            int? id = (int?)cmd.ExecuteScalar();

            _connection.Close();

            return id ?? -1;
        }

        public override bool Delete(int id)
        {
            IDbCommand cmd = _connection.CreateQueryCommand("DELETE FROM [Message] WHERE [Message_Id] = @Id");
            cmd.AddParam("@Id", id);

            _connection.Open();

            bool rowDeleted = cmd.ExecuteNonQuery() == 1;

            _connection.Close();

            return rowDeleted;
        }

        public override IEnumerable<Message> GetAll()
        {
            IDbCommand cmd = _connection.CreateQueryCommand("SELECT * FROM [Message]");

            _connection.Open();

            using (IDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                    yield return Mapper(reader);

            _connection.Close();
        }

        public override Message? GetById(int id)
        {
            IDbCommand cmd = _connection.CreateQueryCommand("SELECT * FROM [Message] WHERE  [Message_Id] = @Id");
            cmd.AddParam("@Id", id);

            int cpt = 0;
            Message? message = null;

            _connection.Open();

            using (IDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    cpt++;
                    message = Mapper(reader);
                }

            _connection.Close();
            if (cpt > 1)
                throw new Exception("On a pas trouvé, sorry");
            return message;
        }

        public override bool Update(int id, Message entity)
        {
            IDbCommand cmd = _connection.CreateQueryCommand("UPDATE [Message] SET [Content] = @Content WHERE [Message_Id] = @Id");

            cmd.AddParam("@Content", entity.Content);
            cmd.AddParam("@Id", id);

            _connection.Open();

            bool rowUpdated = cmd.ExecuteNonQuery() == 1;

            _connection.Close();

            return rowUpdated;
        }


        protected override Message Mapper(IDataRecord record)
        {
            return new Message()
            {
                MessageId = (int)record["Message_Id"],
                Content = (string)record["Content"],
                CreateAt = (DateTime)record["Creat_At"],
                MemberId = (int)record["Member_id"],
            };
        }
    }
}
