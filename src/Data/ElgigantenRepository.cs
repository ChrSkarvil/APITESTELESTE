using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreCodeCamp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreCodeCamp.Data
{
    public class ElgigantenRepository : IElgigantenRepository
    {
        private readonly ElgigantenContext _context;
        private readonly ILogger<ElgigantenRepository> _logger;

        public ElgigantenRepository(ElgigantenContext context, ILogger<ElgigantenRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<ProductInfo[]> GetAllProductInfosAsync()
        {
            _logger.LogInformation($"Getting all Product informations");

            //IQueryable<ProductInfo> query = _context.ProductInfo;

            var query = _context.ProductInfo;

            return await query.ToArrayAsync();
        }

        public async Task<ProductInfo> GetProductInfoAsync(long? ean, long? gtin)
        {
            if (ean != 0)
            {
                _logger.LogInformation($"Getting Product info for {ean}");

                IQueryable<ProductInfo> query = _context.ProductInfo;
                // Query It
                query = query.Where(c => c.EAN == ean);
            return await query.FirstOrDefaultAsync();
            }
            else
            {
                _logger.LogInformation($"Getting Product info for {gtin}");

                IQueryable<ProductInfo> query = _context.ProductInfo;
                // Query It
                query = query.Where(c => c.GTIN == gtin);
                return await query.FirstOrDefaultAsync();
            }

        }

        //query = ((IQueryable<Product>)(from p in _context.Product.AsQueryable()
        //                               join pi in _context.ProductInfo.AsQueryable() on p.ProductInfo.ProductInfoID equals pi.ProductInfoID
        //                               where p.ProductInfo.ProductInfoID == pi.ProductInfoID
        //                               select new
        //                                       {
        //                                           ProductInfoID = pi.ProductInfoID,
        //                                       }));





        public async Task<Product[]> GetAllProductsAsync(bool includeProductInfos = false)
        {
            _logger.LogInformation($"Getting Products");



            IQueryable<Product> query = _context.Product;   

            if (includeProductInfos)
            {
                ////query = query.Include(c => c.ProductInfos);
                //query = ((IQueryable<Product>)(from p in _context.Product
                //                               join pi in _context.ProductInfo on p.ProductInfo.ProductInfoID equals pi.ProductInfoID
                //                               where p.ProductInfo.ProductInfoID == pi.ProductInfoID
                //                               select p.ProductInfos).ToArray());

                //ProductInfo pil = new ProductInfo();



                //var pil = (from p in _context.Product
                //           join pi in _context.ProductInfo on p.ProductInfo.ProductInfoID equals pi.ProductInfoID
                //           select p.ProductInfos)
                           /*where p.ProductInfo.ProductInfoID == pi.ProductInfoID select p.ProductInfos).ToList()*/;
                //{
                //               pi.ProductDescription,
                //               pi.NetDimensions,
                //               pi.NetWeight,
                //               pi.GrossDimensions,
                //               pi.GrossWeight,
                //               pi.EAN,
                //               pi.GTIN
                //           }).ToList();
                //query = query.Include(pil.ToString());


                //public ActionResult Index()
                //{
                //    var product = new Product();
                //    var prices = new Price();
                //    var orders = new Order();
                //    List<ProductRegistrationViewModelVM> vm = new List<ProductRegistrationViewModelVM>();
                //    vm = (from p in db.Products
                //          join i in db.Images on p.ProductId equals i.Product_Id
                //          join s in db.Specifications on p.ProductId equals s.Product_Id
                //          select new ProductRegistrationViewModelVM()
                //          {
                //              //Product
                //              Name = p.Name,
                //              Produt_Code = p.Produt_Code,
                //              Description = p.Description,
                //              //Image
                //              Image_Description = i.Image_Description,
                //              Image_Name = i.Image_Name,
                //              image1 = i.image1,
                //              //Specifications
                //              Sz_Measurement_Unit = s.Sz_Measurement_Unit,
                //              Size = s.Size,
                //              Wg_Measurement_Unit = s.Wg_Measurement_Unit,
                //              Weight = s.Weight,
                //              Price_Set = s.Price_Set,
                //              Price_Sold = s.Price_Sold
                //          }).ToList();

                //    return View(vm);
                //}

                List<ProductInfo> pr = new List<ProductInfo>();
                pr = (from p in _context.Product.AsQueryable()
                      from pi in _context.ProductInfo.AsQueryable()
                      where p.ProductInfo.ProductInfoID == pi.ProductInfoID
                      select new ProductInfo()
                      {
                          ProductInfoID = pi.ProductInfoID,
                          ProductDescription = pi.ProductDescription,
                          NetDimensions = pi.NetDimensions,
                          NetWeight = pi.NetWeight,
                          GrossDimensions = pi.GrossDimensions,
                          GrossWeight = pi.GrossWeight,
                          EAN = pi.EAN,
                          GTIN = pi.GTIN
                      }).ToList();

                //Product tt = new Product();

                //tt.ProductInfos = pr;

                //foreach (ProductInfo pi in pr)
                //{
                //   tt.ProductInfos.Add(pi);
                //}
                //var test = tt.ProductInfos.Add(pr)
                //var test = pr.Concat(tt.ProductInfos);

                query = (IQueryable<Product>)pr;


                //BRUG DET HER HVIS DET ANDET FAILER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //var test = from p in _context.Product
                //           from pi in _context.ProductInfo
                //           where p.ProductInfo.ProductInfoID == pi.ProductInfoID
                //           select new
                //           {
                //              ProductDescription = pi.ProductDescription,
                //              NetDimensions = pi.NetDimensions,
                //              NetWeight = pi.NetWeight,
                //              GrossDimensions = pi.GrossDimensions,
                //              GrossWeight = pi.GrossWeight,
                //              EAN = pi.EAN,
                //              GTIN = pi.GTIN
                //           };

                //foreach (var product in test)
                //{
                //    test.ToString();
                //}

                //query = query.Include(test);

                //List<ProductInfo> pil = (from p in _context.Product
                //                         join pi in _context.ProductInfo on p.ProductInfo.ProductInfoID equals pi.ProductInfoID
                //                         select new ProductInfo
                //                        {
                //                            pi.ProductDescription,
                //                            pi.NetDimensions,
                //                            pi.NetWeight,
                //                            pi.GrossDimensions,
                //                            pi.GrossWeight,
                //                            pi.EAN,
                //                            pi.GTIN
                //                        }).ToList();


                //List<ProductRegistrationViewModelVM> vm = (from p in db.Products
                //                                           join i in db.Images on p.ProductId equals i.Product_Id
                //                                           join s in db.Specifications on p.ProductId equals s.Product_Id
                //                                           select new ProductRegistrationViewModelVM
                //   {
                //       //Product
                //       p.Name,
                //       p.Produt_Code,
                //       p.Description,
                //       //Image
                //       i.Image_Description,
                //       i.Image_Name,
                //       i.image1,
                //       //Specifications
                //       s.Sz_Measurement_Unit,
                //       s.Size,
                //       s.Wg_Measurement_Unit,
                //       s.Weight,
                //       s.Price_Set,
                //       s.Price_Sold
                //   }).ToList();

                //query.Include(c => c.ProductInfos)
            }

            //var query = _context.Product;

            return await query.ToArrayAsync();
        }


        public async Task<Product> GetProductAsync(string productName)
        {
            _logger.LogInformation($"Getting Product");

            var query = _context.Product
              .Where(t => t.ProductName == productName);

            return await query.FirstOrDefaultAsync();
        }
    }
}
