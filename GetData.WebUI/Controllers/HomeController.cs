using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GetData.WebUI.Models;
using GetData.Domain.Entities;
using System.Net;
using GetDataInfo.Domain.Interfaces;

namespace GetData.WebUI.Controllers;

public class HomeController : Controller
{
    public DataInfo DataInfo { get; private set; }
    private readonly ILogger<HomeController> _logger;
    private readonly IDataInfoRepository _dataInfoRepository;

    public HomeController(ILogger<HomeController> logger, IDataInfoRepository dataInfoRepository)
    {
        _logger = logger;
        _dataInfoRepository = dataInfoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        
        DataInfo dataInfo = SetDataInfo();
        ViewData["DataInfo"] = dataInfo;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Privacy()
    {
        
        DataInfo dataInfo = SetDataInfo();

        if(ModelState.IsValid)
        {
            await _dataInfoRepository.Create(dataInfo);
            return RedirectToAction(nameof(Index));
        }
        return View(dataInfo);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private DataInfo SetDataInfo()
    {
        string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
        var externalIp = IPAddress.Parse(externalIpString);
        string localIpv4 = Response.HttpContext.Connection.RemoteIpAddress.ToString();
                
        string[] ipSplit = localIpv4.Split(':');
        var ipSplitLength = ipSplit.Length - 1;
        if(ipSplit[ipSplitLength] == "1")
            localIpv4 = "127.0.0.1";
        else
            localIpv4 = ipSplit[ipSplitLength]; 


        DataInfo = new DataInfo
        (
            localIpv4
            ,externalIp.ToString()
        );
        DataInfo.GetHostname();
        DataInfo.GetMac();
        return DataInfo;
    }
}
