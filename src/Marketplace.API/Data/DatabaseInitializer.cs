using System.Security.Claims;
using Marketplace.API.Data.Contracts;
using Marketplace.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly UserManager<User> userManager;
        private readonly DatabaseContext context;

        public DatabaseInitializer(UserManager<User> userManager, DatabaseContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public void InitializeSeedUsersAuthentication(out User? user)
        {
            user = context.Users
              .AsNoTracking()
              .Where(u => ! string.IsNullOrEmpty(u.Email) && u.Email.Equals("developer@marketplace.com"))
              .Select(u => new User { Id = u.Id})
              .FirstOrDefault();

            if (user is null)
            {
                user = new User {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    NormalizedUserName = "DEVELOPER",
                    UserName = "Developer",
                    Email = "developer@marketplace.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789"
                };

                var result = userManager.CreateAsync(user, "developer@2022").Result;

                if (result.Succeeded)
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));

                    result = userManager.AddClaimsAsync(user, claims).Result;
                }
            }
        }

        public void InitializeSeedUserStores(User user, out Store? store)
        {
            store = context.Stores
              .AsNoTracking()
              .Where(s => s.UserId.Equals(user.Id))
              .Select(s => new Store { Id = s.Id})
              .FirstOrDefault();

            if (store is null)
            {
              store = new Store 
              {
                Id = Guid.Parse("d8248594-84c9-48ca-c7fa-08daa87e0632"), 
                UserId = user.Id, 
                Name = "A4U Weapons", 
                Joined = DateTime.UtcNow
              };
              context.Stores.Add(store);
              context.SaveChanges();
            }
        }

        public void InitializeSeedStoreProducts(Store store, out IList<Product> products)
        {
            var empty = ! context.Products
              .AsNoTracking()
              .Any();

            if (empty)
            {
                products = new List<Product>()
                {
                    new Product { 
                      Id = Guid.Parse("b1af6730-e6f2-4640-8319-9cfbcb095128"),
                      Name = "Pistola Stoeger By.Beretta STR-9 Cal. 9mm Oxidada", 
                      Description = "Stoeger traz uma reputação de espingardas duráveis e confiáveis para o mundo das pistolas semiautomáticas com a nova STR-9. Esta 9mm disparada por Striker vem com recursos que você esperaria de uma pistola que custa o dobro. Seu trilho integrado, segurança interna, liberação de magazine reversível, serrilhados deslizantes otimizados, sistema de visão de três pontos e ergonomia aprimorada fornecem desempenho e conforto consistentes.",
                      Price = 8900.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("b461e2df-2b3e-4501-a9b5-2da32fdb4dbd"),
                      Name = "Pistola Taurus G3c T.O.R.O. - 9x19mm", 
                      Description = "Com o aumento da demanda por red dots em pistolas, tanto para uso de esporte, quanto o uso para defesa, a Taurus lançou no Brasil a G3C T.O.R.O. Uma pistola em tamanho full-size, pronta para a instalação de um red dot de sua escolha, sem necessidade de levar a  pistola á um armeiro. A série G3 é uma melhoria da conhecida G2C. nessa versão em específico, com cano de 3,2\"  e três carregadores com capacidade para 12 tiros.",
                      Price = 5400.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("65739da5-eacc-420d-8dcf-a77b98944f37"),
                      Name = "Tanfoglio Witness 1911", 
                      Description = "Pistola Tanfoglio Witness 1911 Cal .45 ACP Aço Oxidado Talas em Madeira",
                      Price = 12600.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("a1057aca-b32e-4ce2-918e-99875ec78362"),
                      Name = "Pistola Beretta APX", 
                      Description = "A nova pistola semiautomática Beretta APX , utilizando uma estrutura de chassi serializada removível, pode ser facilmente modificada com carcaças de estrutura de punho substituíveis e é simples de desmontar e manter. A facilidade de uso era o principal impulsionador no desenvolvimento do APX.",
                      Price = 12200.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("d49b2702-2541-464d-a7c5-4fe3194aab1c"),
                      Name = "Sig Sauer P320-M18 Coyote", 
                      Description = "O M18 foi recentemente lançado para as Forças Armadas dos EUA e foi escolhido como a arma oficial do Corpo de Fuzileiros Navais dos EUA. O P320-M18 é a versão comercial que traz a mesma precisão sem precedentes, extrema confiabilidade e durabilidade incomparável que as forças armadas exigem.",
                      Price = 16990.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("c3e278c7-952f-4500-8c79-0bef7f5d583d"),
                      Name = "Springfield XD-M tactico OSP", 
                      Description = "O Springfield Armory XD-M Elite 4.5 “Tactical OSP ™ em Flat Dark Earth (FDE) reúne todos os recursos avançados da linha Elite em uma potência de 9 mm pronta para operações. Ostentando um cano rosqueado estendido, uma capacidade de aceitar os atuais top red dot óptico, mira de supressor de ferro de altura, o Match Enhanced Trigger Assembly (META), batente de slide ambidestro, serrilhas de slide aprimoradas, o O XD-M Elite 4.5 “OSP ™ tático oferece o auge de desempenho.",
                      Price = 13290.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("b7839e89-63db-498b-ab82-9b4bd31017c4"),
                      Name = "Pistola Taurus TX22 Cal. 22LR Oxidada - 16 Tiros", 
                      Description = "Pistola calibre .22 LR mais avançada do mercado. Robusta, porém leve e com design moderno que permite facilidade de operação. Com capacidade de 16 tiros, armação em polímero de alta resistência e tamanho full size, é ideal para o tiro esportivo, tanto para iniciantes como para atiradores experientes. A Pistola TAURUS TX22 foi eleita pela Revista americana Guns & Ammo como a arma do ano em 2019.",
                      Price = 7400.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("e3e9065e-0635-4b57-8991-7775ba6d1568"),
                      Name = "Pistola Taurus .9MM TS9/17 4\" CAFO", 
                      Description = "Robusta, confiável, segura e de fácil operação. Possui sistema de ação com percussor lançado – striker, mecanismo com o exclusivo sistema de segurança de dupla trava de gatilho (trava do gatilho e trava manual), trava do percussor e trava de queda, que aliados ao mecanismo de disparo e design inovador, asseguram a praticidade de pronto emprego e a facilidade de manutenção.",
                      Price = 7100.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("aa10b7bc-6343-4838-9caf-91c5ae1c98eb"),
                      Name = "Revólver Taurus .357 MAG RT608/8 6,5\" INAB", 
                      Description = "Calibre: .357 MAG\nComprimento Total:360mm\nComprimento do Cano: 6,5\" (165mm)\nCapacidade: 8 tiros\nAcabamento: Inox Alto Brilho\nAção: SA/DA\nMira: Alça Regulável\nPeso: 1460g",
                      Price = 6600.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("61756887-0ee6-42b8-aa68-0aab609be6cf"),
                      Name = "Carabina Taurus CT9G2 9mm Polímero Alumínio Anodisado Preto", 
                      Description = "A carabina CT9 é uma arma tática ideal para a prática esportiva. Possui capacidade de 30 / 32 tiros, calibre 9mm e cano de 412mm com ação simples e semiautomática com teclas ambidestras. Esse modelo vem equipado com trilho Picatinny integral na caixa da culatra para incluir acessórios, massa e vértice de mira ajustáveis. A coronha é fixa no modelo standard, porém acompanha uma coronha rebatível que pode ser facilmente trocada para melhorar a ergonomia e a mobilidade no tiro. Essa arma é perfeita para o uso no tiro esportivo, pois tem as mesmas características da CTT 40, porém com o consagrado calibre 9×19, aliado a um cano de 16″ de comprimento, proporcionando maior precisão e alto desempenho para o atirador.",
                      Price = 13000.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("fce95fb5-4a75-489b-aaca-6a5b0d922ad5"),
                      Name = "Rifle Cbc 8122 Bolt Action Cal .22lr - Cano 23\" - 10 Tiros - Oxidado - Polímero", 
                      Description = "O Rifle 8122 possui arrojada coronha thumbhole (vazada) em polímero (polipropileno, enriquecido com fibra de vidro), que garante grande leveza e resistência à arma. Sua coronha possui ainda suporte para bandoleira e trilho picatinny integrado, que possibilita o acoplamento de acessórios ópticos com este sistema de fixação. No receptáculo, possui o tradicional sistema de fixação \"rabo de andorinha\".",
                      Price = 2700.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("d31d431f-5bf3-46d8-b879-c1c2f7c96de8"),
                      Name = "Espingarda Pump CBC Military 3.0 RT 12/24 SEM ACESSÓRIOS", 
                      Description = "A Pump Military 3.0 CBC foi a primeira espingarda calibre 12 homologada pelo Exército Brasileiro como MEM – Material de Emprego Militar. Esta importante certificação é composta por vários critérios e etapas de avaliação, abrangendo rigorosos testes de desempenho e de resistência em condições extremas.O alto desempenho e resistência da Pump Military 3.0 CBC foram comprovados nos testes do processo de certificação MEM, realizados por várias Organizações Militares localizadas em diferentes regiões do País, em condições adversas e em diversos tipos de ambiente operacional, como selva e caatinga.",
                      Price = 6100.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("00c1c882-1fcb-47ec-b5c8-1b8f966564e5"),
                      Name = "COLDRE KYDEX IWB INVICTUS TAURUS SÉRIE 100", 
                      Description = "O Coldre Kydex® para Plataforma Taurus® Iwb Destro SÉRIE 100 INVICTUS têm tecnologia Kydex® USA, um produto diferenciado no meio tático, para operadores e operadoras que exigem o melhor, sempre. O Kydex® não deforma, não propaga chamas e é extremamente resistente ao impacto. Com duas opções de clip, um em polímero (POM), muito resistente, e outro clip metálico em aço inoxidável, com ajuste de ângulo. Ambos os clips possuem regulagem de altura. Os modelos ainda contam com redutor de volume, que pode ser retirado se necessário, e ajuste de retenção, com kit extra de parafusos.",
                      Price = 299.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                    new Product { 
                      Id = Guid.Parse("9e170173-20c4-422d-ba70-f92694e29254"),
                      Name = "COLDRE GLOCK INVICTUS KYDEX IWB DESTRO STANDARD", 
                      Description = "O Coldre GLOCK® Kydex® Iwb Destro Standard INVICTUS têm tecnologia Kydex® USA, um produto diferenciado no meio tático, para operadores e operadoras que exigem o melhor, sempre. O Kydex® não deforma, não propaga chamas e é extremamente resistente ao impacto. Com duas opções de clip, um em polímero (POM), muito resistente, e outro clip metálico em aço inoxidável, com ajuste de ângulo. Ambos os clips possuem regulagem de altura. Os modelos ainda contam com redutor de volume, que pode ser retirado se necessário, e ajuste de retenção, com kit extra de parafusos.",
                      Price = 299.00M,
                      Stock = 50,
                      CategoryId = 1026,
                      StoreId = store.Id
                    },
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            products = Array.Empty<Product>();
        }
    }
}