using BlazorApp1.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp1.Server.Data.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasMany(user => user.Chatrooms)
            .WithMany(chatroom => chatroom.Users)
            .UsingEntity(e => e.ToTable("UsersInChats"));

        builder.HasMany(user => user.Messages)
            .WithOne(message => message.Sender)
            .HasForeignKey(message => message.SenderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}