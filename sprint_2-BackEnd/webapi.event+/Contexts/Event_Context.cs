﻿using Microsoft.EntityFrameworkCore;
using webapi.event_.Domains;

namespace webapi.event_.Contexts
{
    /// <summary>
    /// Classe de contexto que faz referências as tabelas e define string de conexão
    /// </summary>
    public class Event_Context : DbContext
    {
        public DbSet<TiposUsuario> TiposUsuario { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<TiposEvento> TiposEvento { get; set; }

        public DbSet<Evento> Evento { get; set; }

        public DbSet<ComentariosEvento> ComentariosEvento{ get; set; }

        public DbSet<Instituicao> Instituicao { get; set; }

        public DbSet<PresencasEvento> PresencasEvento { get; set; }

        /// <summary>
        /// Define as opções de construção do banco
        /// </summary>
        /// <param name="optionsBuilder">Objeto com as configurações definidas</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-16UD9N2; Database= event+_manha; Integrated Security=True; TrustServerCertificate=true;");
            base.OnConfiguring(optionsBuilder);
        }
        //optionsBuilder.UseSqlServer("Server=tcp:eventmanhaenzo-server.database.windows.net,1433;Initial Catalog=event_manha_database_enzo;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;User Id = eventmanhaenzo-server; Pwd = Senai@134;");
        //base.OnConfiguring(optionsBuilder);
    }
}
