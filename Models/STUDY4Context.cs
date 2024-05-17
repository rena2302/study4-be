using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace study4_be.Models
{
    public partial class STUDY4Context : DbContext
    {
        public STUDY4Context()
        {
        }

        public STUDY4Context(DbContextOptions<STUDY4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Audio> Audios { get; set; } = null!;
        public virtual DbSet<Container> Containers { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vocabulary> Vocabularies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-62MKG1UJ;Initial Catalog=STUDY4;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("ANSWER");

                entity.Property(e => e.AnswerId)
                    .ValueGeneratedNever()
                    .HasColumnName("ANSWER_ID");

                entity.Property(e => e.Answer1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER");

                entity.Property(e => e.QuestionId).HasColumnName("QUESTION_ID");

                entity.Property(e => e.QuizzesId).HasColumnName("QUIZZES_ID");

                entity.Property(e => e.UsersId)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USERS_ID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_ANSWER_QUESTION");

                entity.HasOne(d => d.Quizzes)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuizzesId)
                    .HasConstraintName("FK_ANSWER_QUIZZES");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK_ANSWER_USERS");
            });

            modelBuilder.Entity<Audio>(entity =>
            {
                entity.ToTable("AUDIO");

                entity.Property(e => e.AudioId)
                    .ValueGeneratedNever()
                    .HasColumnName("AUDIO_ID");

                entity.Property(e => e.AudioDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("AUDIO_DESCRIPTION");

                entity.Property(e => e.AudioUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("AUDIO_URL");

                entity.Property(e => e.LessonsId).HasColumnName("LESSONS_ID");

                entity.HasOne(d => d.Lessons)
                    .WithMany(p => p.Audios)
                    .HasForeignKey(d => d.LessonsId)
                    .HasConstraintName("FK_AUDIO_LESSONS");
            });

            modelBuilder.Entity<Container>(entity =>
            {
                entity.ToTable("Container");

                entity.Property(e => e.ContainerId)
                    .ValueGeneratedNever()
                    .HasColumnName("Container_id");

                entity.Property(e => e.FlashCardId).HasColumnName("FlashCard_id");

                entity.Property(e => e.GrammarId).HasColumnName("Grammar_id");

                entity.Property(e => e.LessonId).HasColumnName("Lesson_id");

                entity.Property(e => e.TrainerId).HasColumnName("Trainer_id");

                entity.Property(e => e.VideoId).HasColumnName("Video_id");

                entity.Property(e => e.VocabId).HasColumnName("Vocab_id");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Containers)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK_Container_Lessons");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CoursesId)
                    .HasName("PK_COURSE");

                entity.ToTable("COURSES");

                entity.Property(e => e.CoursesId)
                    .ValueGeneratedNever()
                    .HasColumnName("COURSES_ID");

                entity.Property(e => e.CourseDescription)
                    .HasMaxLength(255)
                    .HasColumnName("COURSE_DESCRIPTION");

                entity.Property(e => e.CourseImage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COURSE_IMAGE");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(100)
                    .HasColumnName("COURSE_NAME");

                entity.Property(e => e.CoursePrice).HasColumnName("COURSE_PRICE");

                entity.Property(e => e.CourseTag)
                    .HasMaxLength(100)
                    .HasColumnName("COURSE_TAG");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.LessonsId);

                entity.ToTable("LESSONS");

                entity.Property(e => e.LessonsId)
                    .ValueGeneratedNever()
                    .HasColumnName("LESSONS_ID");

                entity.Property(e => e.Content)
                    .HasMaxLength(100)
                    .HasColumnName("CONTENT");

                entity.Property(e => e.CoursesId).HasColumnName("COURSES_ID");

                entity.Property(e => e.LessonsTitle)
                    .HasMaxLength(200)
                    .HasColumnName("LESSONS_TITLE");

                entity.HasOne(d => d.Courses)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.CoursesId)
                    .HasConstraintName("FK_LESSONS_COURSES");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Order_id");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CourseId).HasColumnName("Course_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_date");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(10, 3)")
                    .HasColumnName("Total_amount");

                entity.Property(e => e.UsersId)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("Users_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Orders_Courses");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("QUESTION");

                entity.Property(e => e.QuestionId)
                    .ValueGeneratedNever()
                    .HasColumnName("QUESTION_ID");

                entity.Property(e => e.CorrectAnswer)
                    .HasMaxLength(100)
                    .HasColumnName("CORRECT_ANSWER");

                entity.Property(e => e.IdQuizzes).HasColumnName("ID_QUIZZES");

                entity.Property(e => e.QuestionText)
                    .HasMaxLength(100)
                    .HasColumnName("QUESTION_TEXT");

                entity.HasOne(d => d.IdQuizzesNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.IdQuizzes)
                    .HasConstraintName("FK_QUESTION_QUIZZES");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.QuizzesId);

                entity.ToTable("QUIZZES");

                entity.Property(e => e.QuizzesId)
                    .ValueGeneratedNever()
                    .HasColumnName("QUIZZES_ID");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_TIME");

                entity.Property(e => e.DescriptionQuizzes)
                    .HasMaxLength(100)
                    .HasColumnName("DESCRIPTION_QUIZZES");

                entity.Property(e => e.LessonsId).HasColumnName("LESSONS_ID");

                entity.Property(e => e.Title)
                    .HasMaxLength(70)
                    .HasColumnName("TITLE");

                entity.HasOne(d => d.Lessons)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.LessonsId)
                    .HasConstraintName("FK_QUIZZES_LESSONS");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("RATING");

                entity.Property(e => e.RatingId)
                    .ValueGeneratedNever()
                    .HasColumnName("RATING_ID");

                entity.Property(e => e.CoursesId).HasColumnName("COURSES_ID");

                entity.Property(e => e.RatingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("RATING_DATE");

                entity.Property(e => e.RatingValue).HasColumnName("RATING_VALUE");

                entity.Property(e => e.Review)
                    .HasMaxLength(200)
                    .HasColumnName("REVIEW");

                entity.Property(e => e.UsersId)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USERS_ID");

                entity.HasOne(d => d.Courses)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.CoursesId)
                    .HasConstraintName("FK_RATING_COURSES");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK_RATING_USERS");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsersId);

                entity.ToTable("USERS");

                entity.Property(e => e.UsersId)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USERS_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.UsersBanner)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("USERS_BANNER");

                entity.Property(e => e.UsersDescription)
                    .HasMaxLength(100)
                    .HasColumnName("USERS_DESCRIPTION");

                entity.Property(e => e.UsersImage)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("USERS_IMAGE");

                entity.Property(e => e.UsersName)
                    .HasMaxLength(70)
                    .HasColumnName("USERS_NAME");

                entity.Property(e => e.UsersPassword)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USERS_PASSWORD");
            });

            modelBuilder.Entity<Vocabulary>(entity =>
            {
                entity.HasKey(e => e.VocabId);

                entity.ToTable("VOCABULARY");

                entity.Property(e => e.VocabId)
                    .ValueGeneratedNever()
                    .HasColumnName("VOCAB_ID");

                entity.Property(e => e.AudioUrl)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Audio_URL");

                entity.Property(e => e.Example)
                    .IsUnicode(false)
                    .HasColumnName("EXAMPLE");

                entity.Property(e => e.Explanation)
                    .IsUnicode(false)
                    .HasColumnName("EXPLANATION");

                entity.Property(e => e.LessonsId).HasColumnName("LESSONS_ID");

                entity.Property(e => e.Mean)
                    .HasMaxLength(60)
                    .HasColumnName("MEAN");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Vocab)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VOCAB");

                entity.HasOne(d => d.Lessons)
                    .WithMany(p => p.Vocabularies)
                    .HasForeignKey(d => d.LessonsId)
                    .HasConstraintName("FK_VOCABULARY_LESSONS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
