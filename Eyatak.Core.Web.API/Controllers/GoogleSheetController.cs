﻿using DomainLayer.Concrete.Google.Sheets.Domain;
using Etsy.Google.Sheets.Concrete.Helpers;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace Eyatak.Core.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class GoogleSheetController : ControllerBase
    {
        const string SPREADSHEET_ID = "1ahtrdr40eR23-S1MQQOYncvuVmOIcPyGiZ3xjS_SDTQ";
        const string SHEET_NAME = "EtsyOrders";
        SpreadsheetsResource.ValuesResource _googleSheetValues;
        public GoogleSheetController(GoogleSheetsHelper googleSheetsHelper)
        {
            _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var range = $"{SHEET_NAME}!A:D";
            var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
            var response = request.Execute();
            var values = response.Values;
            return Ok(ItemsMapper.MapFromRangeData(values));
        }
        [HttpGet("{rowId}")]
        public IActionResult Get(int rowId)
        {
            var range = $"{SHEET_NAME}!A{rowId}:D{rowId}";
            var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
            var response = request.Execute();
            var values = response.Values;
            return Ok(ItemsMapper.MapFromRangeData(values).FirstOrDefault());
        }
        [HttpPost]
        public IActionResult Post(Item item)
        {
            var range = $"{SHEET_NAME}!A:D";
            var valueRange = new ValueRange
            {
                Values = ItemsMapper.MapToRangeData(item)
            };
            var appendRequest = _googleSheetValues.Append(valueRange, SPREADSHEET_ID, range);
            appendRequest.ValueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.Execute();
            return CreatedAtAction(nameof(Get), item);
        }
        [HttpPut("{rowId}")]
        public IActionResult Put(int rowId, Item item)
        {
            var range = $"{SHEET_NAME}!A{rowId}:D{rowId}";
            var valueRange = new ValueRange
            {
                Values = ItemsMapper.MapToRangeData(item)
            };
            var updateRequest = _googleSheetValues.Update(valueRange, SPREADSHEET_ID, range);
            updateRequest.ValueInputOption = UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
            return NoContent();
        }
        [HttpDelete("{rowId}")]
        public IActionResult Delete(int rowId)
        {
            var range = $"{SHEET_NAME}!A{rowId}:D{rowId}";
            var requestBody = new ClearValuesRequest();
            var deleteRequest = _googleSheetValues.Clear(requestBody, SPREADSHEET_ID, range);
            deleteRequest.Execute();
            return NoContent();
        }
    }
}
