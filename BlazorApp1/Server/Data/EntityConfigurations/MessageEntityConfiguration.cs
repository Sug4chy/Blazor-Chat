using BlazorApp1.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp1.Server.Data.EntityConfigurations;

public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(message => message.Id);
        builder.HasOne(message => message.Chat)
            .WithMany(chatroom => chatroom.Messages);

        builder.HasOne(message => message.Sender)
            .WithMany(sender => sender.Messages);
    }
}