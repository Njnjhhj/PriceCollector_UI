Preparation

1. Create a folder 'PriceCollectorData' on disk C: .
2. Create inside of folder 'PriceCollectorData' 2 sub-folders: 'Input' and 'Output'. 
3. Put xslx-file with name 'ProductId' into 'Input' folder.
4. 'ProductId' file should contain only one column with header 'Id'. List of product IDs should be under the header.
5. Result of work should be in 'Output' sub-folder of 'PriceCollectorData' folder. File name format: OutputPrice_YYYYMMDD_HHmmss 

Using

1. Open MS Visual Studio.
2. Open 'PriceCollector' project.
3. Find 'PriceCollector.Collector' in Solution Explorer (right panel in Visual Studio) and open it.
4. Find 'Executor_PriceCollector.cs'. Double click.
5. There are 10 test methods:

	- 	Dmlights_collector, 
		Tecshop_collector, 
		Webshop_collector, 
		Solyd_collector, 
		Gigatek_collector, 
		Semmatec_collector, 
		Groothandel_collector, 
		Omnielectric_collector, 
		Zelektro_collector, 
		MyElectro_collector - each of them collects prices from one specific website.
		
	- 	All_Pages_collector - collects prices from all websites.
	
6. Find annotation [Test] above the required method.
7. Right click on the annotation > Left click on 'Run test/s'.
8. Wait for finis of the programm. 