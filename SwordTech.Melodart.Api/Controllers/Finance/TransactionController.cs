using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Finance;
using SwordTech.Melodart.Application.Contract.Finance.Models;

namespace SwordTech.Melodart.Api.Controllers.Finance;

[Route("api/Finance/[controller]")]
public class TransactionController : BaseApiController
{
    private readonly ITransactionAppService _transactionAppService;
    private readonly IAccountAppService _accountAppService;

    public TransactionController(ITransactionAppService transactionAppService, IAccountAppService accountAppService)
    {
        _transactionAppService = transactionAppService;
        _accountAppService = accountAppService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<TransactionDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get()
    {
        var data = await _transactionAppService.GetAll();

        return Success(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<TransactionDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _transactionAppService.GetById(id);

        return Success(data);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<TransactionDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Post([FromBody] TransactionCreateDto model)
    {
        var data = await _accountAppService.AddTransaction(model);

        return Success(data);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<TransactionDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Put(Guid id, [FromBody] TransactionUpdateDto model)
    {
        var data = await _transactionAppService.Update(id, model);

        return Success(data);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _transactionAppService.Delete(id);

        return Success("Deleted successfully.");
    }
}
