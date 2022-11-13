using ChatRoomWithBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatRoomWithBot.Data.Mappings
{
    internal class ChatRoomMappings : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(u => u.Id);

            builder.ToTable("ChatRooms");

        }

    }
}
