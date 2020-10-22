﻿// <auto-generated />
using Glossary.API.DAL.Glossary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Glossary.API.Data.Migrations
{
    [DbContext(typeof(GlossaryContext))]
    [Migration("20200729134805_TermSeedData")]
    partial class TermSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Glossary.API.DAL.Models.GlossaryTerm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Term");

                    b.HasKey("Id");

                    b.ToTable("GlossaryTerms");
                });
#pragma warning restore 612, 618
        }
    }
}
