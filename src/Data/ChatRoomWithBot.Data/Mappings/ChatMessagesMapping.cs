using ChatRoomWithBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatRoomWithBot.Data.Mappings
{
    internal class ChatMessagesMapping : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(u => u.Id);

             
        }
    }
}
