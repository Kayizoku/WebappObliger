using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;

namespace Gruppeoppgave1.DAL.IRepositories
{
    public interface IStasjonRepository
    {
        Task<List<Stasjon>> HentAlleStasjoner();
        Task<Stasjon> HentEnStasjon(int id);
        Task<bool> EndreStasjon(Stasjon stasjon);
        Task<bool> FjernStasjon(int id);
        Task<bool> LagreStasjon(Stasjon stasjon);
        
    }
}
