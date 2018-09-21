using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ITPPro.Models;
using ITPPro.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITPPro.Data
{
    public class BaseRepository : DbContext
    {
        public BaseRepository() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BaseRepository>(null);
            ConfigureDarboUzklausos(modelBuilder);
            ConfigureDarbuotojai(modelBuilder);
            ConfigureDarbuotojuTipai(modelBuilder);
            ConfigureKambariai(modelBuilder);
            ConfigureKambarioTipai(modelBuilder);
            ConfigureKlientas(modelBuilder);
            ConfigureMokejimai(modelBuilder);
            ConfigureMokejimoBusenos(modelBuilder);
            ConfigureMokejimoTipai(modelBuilder);
            ConfigurePapildomosPaslaugos(modelBuilder);
            ConfigureRezervacijos(modelBuilder);
            ConfigureRezervacijosBusena(modelBuilder);
            ConfigureRezervacijosKambariai(modelBuilder);
            ConfigureRezervacijosPapildomosPaslaugos(modelBuilder);
            ConfigureTeises(modelBuilder);
            ConfigureTeisiuTipai(modelBuilder);
            ConfigureViesbuciai(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureKlientas(DbModelBuilder builder)
        {
            var model = builder.Entity<Klientas>();
            model.ToTable("KLIENTAI");

            model.Property(p => p.kliento_kodas).HasColumnName("kliento_kodas").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.vardas).HasColumnName("vardas");
            model.Property(p => p.pavarde).HasColumnName("pavarde");
            model.Property(p => p.el_pastas).HasColumnName("el_pastas");
            model.Property(p => p.lytis).HasColumnName("lytis");
            model.Property(p => p.telefonas).HasColumnName("telefonas");
            model.Property(p => p.adresas).HasColumnName("adresas");
            model.Property(p => p.sukurimo_data).HasColumnName("sukurimo_data");
            model.Property(p => p.slaptazodis).HasColumnName("slaptazodis");
        }

        public void ConfigureMokejimoBusenos(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Mokejimo_Busenos_Enum>();
            model.ToTable("MOKEJIMO_BUSENOS");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.name).HasColumnName("name");
        }

        public void ConfigureMokejimoTipai(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Mokejimo_Tipai_Enum>();
            model.ToTable("MOKEJIMO_TIPAI");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.name).HasColumnName("name");
        }

        public void ConfigurePapildomosPaslaugos(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Papildoma_paslauga>();
            model.ToTable("PAPILDOMOS_PASLAUGOS");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.aprasymas).HasColumnName("aprasymas");
            model.Property(p => p.kaina).HasColumnName("kaina");
            model.Property(p => p.fk_Viesbutisid).HasColumnName("fk_Viesbutisid");
        }

        public void ConfigureKambarioTipai(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Kambario_Tipai_Enum>();
            model.ToTable("KAMBARIO_TIPAI");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.name).HasColumnName("name");
        }

        public void ConfigureDarbuotojuTipai(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Darbuotoju_Tipai_Enum>();
            model.ToTable("DARBUOTOJU_TIPAI");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.name).HasColumnName("name");
        }

        public void ConfigureTeisiuTipai(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Teisiu_Tipo_Enum>();
            model.ToTable("TEISIU_TIPAI");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.name).HasColumnName("name");
        }

        public void ConfigureRezervacijosPapildomosPaslaugos(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Rezervacijos_papildoma_paslauga>();
            model.ToTable("REZERVACIJOS_PAPILDOMOS_PASLAUGOS");

            model.Property(p => p.fk_Rezervacijaid).HasColumnName("fk_Rezervacijaid");
            model.Property(p => p.fk_Papildoma_paslaugaid).HasColumnName("fk_Papildoma_paslaugaid");
        }

        public void ConfigureRezervacijosKambariai(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Rezervacijos_kambarys>();
            model.ToTable("REZERVACIJOS_KAMBARIAI");

            model.Property(p => p.fk_Rezervacijaid).HasColumnName("fk_Rezervacijaid");
            model.Property(p => p.fk_Kambarysid).HasColumnName("fk_Kambarysid");
        }

        public void ConfigureRezervacijosBusena(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Rezervacijos_Busena_Enum>();
            model.ToTable("Rezervacijos_busena");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.name).HasColumnName("name");
        }

        private void ConfigureRezervacijos(DbModelBuilder builder)
        {
            var model = builder.Entity<Rezervacija>();
            model.ToTable("REZERVACIJOS");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.rezervacijos_pradzia).HasColumnName("rezervacijos_pradzia");
            model.Property(p => p.rezervacijos_pabaiga).HasColumnName("rezervacijos_pabaiga");
            model.Property(p => p.rezervacijos_atlikimo_data).HasColumnName("rezervacijos_atlikimo_data");
            model.HasOptional(o => o.busena).WithMany().Map(m => m.MapKey("busena"));
            model.Property(p => p.fk_Klientaskliento_kodas).HasColumnName("fk_Klientaskliento_kodas");
        }

        private void ConfigureViesbuciai(DbModelBuilder builder)
        {
            var model = builder.Entity<Viesbutis>();
            model.ToTable("VIESBUCIAI");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.pavadinimas).HasColumnName("pavadinimas");
            model.Property(p => p.viesbuciu_tinklas).HasColumnName("viesbuciu_tinklas");
            model.Property(p => p.zvaigzduciu_sk).HasColumnName("zvaigzduciu_sk");
            model.Property(p => p.miestas).HasColumnName("miestas");
            model.Property(p => p.adresas).HasColumnName("adresas");
            model.Property(p => p.aprasymas).HasColumnName("aprasymas");
            model.Property(p => p.fk_savininkas).HasColumnName("fk_savininkas");
        }

        private void ConfigureTeises(DbModelBuilder builder)
        {
            var model = builder.Entity<Teises>();
            model.ToTable("TEISES");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.viesbuciu_tinklas).HasColumnName("viesbuciu_tinklas");
            model.Property(p => p.viesbutis).HasColumnName("viesbutis");
            model.HasOptional(o => o.tipas).WithMany().Map(m => m.MapKey("tipas"));
            model.Property(p => p.fk_Klientaskliento_kodas).HasColumnName("fk_Klientaskliento_kodas");
            model.Property(p => p.fk_Darbuotojasdarbuojo_kodas).HasColumnName("fk_Darbuotojasdarbuojo_kodas");
            model.Property(p => p.teisiu_statusas).HasColumnName("teisiu_statusas");
            model.Property(p => p.data_iki).HasColumnName("data_iki");
            model.Property(p => p.priezastis).HasColumnName("priezastis");
        }

        private void ConfigureMokejimai(DbModelBuilder builder)
        {
            var model = builder.Entity<Mokejimas>();
            model.ToTable("MOKEJIMAI");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.data).HasColumnName("data");
            model.Property(p => p.atsiskaitymo_budas).HasColumnName("atsiskaitymo_budas");
            model.Property(p => p.suma).HasColumnName("suma");
            model.Property(p => p.apmokejimo_data).HasColumnName("apmokejimo_data");
            model.HasOptional(o => o.tipas).WithMany().Map(m => m.MapKey("tipas"));
            model.HasOptional(o => o.busena).WithMany().Map(m => m.MapKey("busena"));
            model.Property(p => p.fk_Klientaskliento_kodas).HasColumnName("fk_Klientaskliento_kodas");
            model.Property(p => p.fk_Rezervacijaid).HasColumnName("fk_Rezervacijaid");
        }

        private void ConfigureKambariai(DbModelBuilder builder)
        {
            var model = builder.Entity<Kambarys>();
            model.ToTable("KAMBARIAI");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.vietu_sk).HasColumnName("vietu_sk");
            model.Property(p => p.numeris).HasColumnName("numeris");
            model.Property(p => p.kaina).HasColumnName("kaina");
            model.Property(p => p.aprasymas).HasColumnName("aprasymas");
            model.HasOptional(o => o.tipas).WithMany().Map(m => m.MapKey("tipas"));
            model.Property(p => p.fk_Viesbutisid).HasColumnName("fk_Viesbutisid");
        }

        private void ConfigureDarboUzklausos(DbModelBuilder builder)
        {
            var model = builder.Entity<Darbo_uzklausa>();
            model.ToTable("DARBO_UZKLAUSOS");

            model.Property(p => p.id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.slaptazodis).HasColumnName("slaptazodis");
            model.Property(p => p.fk_Darbuotojasdarbuojo_kodas).HasColumnName("fk_Darbuotojasdarbuojo_kodas");
            model.Property(p => p.fk_Klientaskliento_kodas).HasColumnName("fk_Klientaskliento_kodas");
            model.Property(p => p.pareigos).HasColumnName("pareigos");
        }

        private void ConfigureDarbuotojai(DbModelBuilder builder)
        {
            var model = builder.Entity<Darbuotojas>();
            model.ToTable("DARBUOTOJAI");

            model.Property(p => p.darbuojo_kodas).HasColumnName("darbuojo_kodas").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            model.Property(p => p.vardas).HasColumnName("vardas");
            model.Property(p => p.pavarde).HasColumnName("pavarde");
            model.Property(p => p.adresas).HasColumnName("adresas");
            model.Property(p => p.telefonas).HasColumnName("telefonas");
            model.Property(p => p.lytis).HasColumnName("lytis");
            model.Property(p => p.darbo_pradzios_laikas).HasColumnName("darbo_pradzios_laikas");
            model.Property(p => p.slaptazodis).HasColumnName("slaptazodis");
            model.HasOptional(o => o.darbuotojo_tipas).WithMany().Map(m => m.MapKey("darbuotojo_tipas"));
            model.Property(p => p.fk_Viesbutisid).HasColumnName("fk_Viesbutisid");
            model.Property(p => p.el_pastas).HasColumnName("el_pastas");
        }




    }
}