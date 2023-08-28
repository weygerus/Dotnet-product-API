using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Infrastructure.Data.Contract.Validations;
using Desafio.Infrastructure.Data.Contract.Interfaces;

namespace Desafio.Infrastructure.Data.Validations
{
    public class ProductValidations : IProductValidations
    {
        private readonly IProductRepository _ProductRepository;

        public ProductValidations(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        }

        public async Task<ProductValidationResponseDTO> ValidateAvailability(Product productToValidate)
        {
            var validationResponse = new ProductValidationResponseDTO();

            if (productToValidate is null)
            {
                validationResponse.IsAvailable = false;
                validationResponse.Message = "Dados do produto não encontrados!";
            }

            if (!await this.ValidateProductName(productToValidate.Name))
            {
                validationResponse.IsAvailable = false;
                validationResponse.Message = "Nome do produto já existe!";
            }

            if (!this.ValidateCategoryIntoProduct(productToValidate))
            {
                validationResponse.IsAvailable = false;
                validationResponse.Message = "Produto precisa de pelo menos uma categoria!";
            }

            validationResponse.IsAvailable = true;

            return validationResponse;
        }

        private async Task<bool> ValidateProductName(string nameToValidate)
        {
            var existingName = await _ProductRepository.GetProductByName(nameToValidate);

            if (existingName is null)
            {
                return true;
            }

            return false;
        }

        private bool ValidateCategoryIntoProduct(Product productToValidate)
        {
            if (productToValidate.ProductCategories.Count is 0)
            {
                return false;
            }

            return true;
        }
    }
}