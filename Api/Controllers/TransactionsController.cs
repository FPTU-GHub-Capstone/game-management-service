﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Diagnostics;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/transactions")]
public class TransactionsController : BaseController
{
    private readonly ITransactionServices _transactionServices;
    private readonly IGenericRepository<TransactionEntity> _transactionRepo;
    public TransactionsController(ITransactionServices transactionServices, IGenericRepository<TransactionEntity> transactionRepo)
    {
        _transactionServices = transactionServices;
        _transactionRepo = transactionRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        return Ok(await _transactionServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransaction(Guid id)
    {
        return Ok(await _transactionServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest trans)
    {
        var newTrans = new TransactionEntity();
        Mapper.Map(trans, newTrans);
        await _transactionServices.Create(newTrans);
        return CreatedAtAction(nameof(GetTransaction),new { id = newTrans.Id }, newTrans );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTransactionRequest trans)
    {
        var updateTrans = await _transactionRepo.FoundOrThrowAsync(id, "Transaction not exist.");
        Mapper.Map(trans, updateTrans);
        await _transactionServices.Update(updateTrans);
        return Ok(updateTrans);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        await _transactionRepo.FoundOrThrowAsync(id, "Transaction not exist.");
        await _transactionServices.Delete(id);
        return NoContent();
    }
}
