using AutoMapper;
using irede.core.Dtos.Core;
using irede.core.Entities;

namespace irede.application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de Produto para ProdutoDto
            CreateMap<Produto, ProdutoDto>()
                .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome));

            // Mapeamento de ProdutoDto para Produto
            CreateMap<ProdutoDto, Produto>()
                .ForMember(dest => dest.Categoria, opt => opt.Ignore()); // Ignorar para evitar sobrescrever a navegação
        }
    }
}
