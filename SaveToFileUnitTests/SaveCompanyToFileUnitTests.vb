Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports SaveToFile
Imports System.IO
Imports NSubstitute

<TestClass()>
Public Class SaveCompanyToFileUnitTests

    Private outputFileExtension As String = "data/"

    <TestMethod()>
    Public Sub SaveCompanyToFile_SaveEmptyCompany()
        Dim nCompany As New Company(New AssetManager(), New SaveCompanyToFile())

        nCompany.Name = "SaveEmptyCompany"
        nCompany.Level = 1
        nCompany.Shares = 100

        nCompany.Save()

        CompareFiles(outputFileExtension & "Expected_SaveEmptyCompany", outputFileExtension & "SaveEmptyCompany")
    End Sub

    <TestMethod()>
    Public Sub SaveCompanyToFile_SaveEngineSettings()
        Dim testSaveSettings As New SaveCompanyToFile()

        Dim gameEngine As New EngineSettings()
        gameEngine.minWage = 1337

        testSaveSettings.SaveGameSettings(gameEngine)
        

        CompareFiles(outputFileExtension & "Expected_GameSettings", outputFileExtension & "GameSettings")
    End Sub

    <TestMethod()>
    Public Sub SaveCompanyToFile_SaveCompanyWithAssests()
        Dim nCompany As New Company(New AssetManager, New SaveCompanyToFile())

        nCompany.Name = "CompanyWithAssets"
        nCompany.Level = 1
        nCompany.Shares = 100

        Dim craftRes As New CraftResource() With {.Name = "CraftResource", .Level = 2, .Shares = 1337}
        Dim LuxRes As New LuxuryResource() With {.Name = "LuxResource", .Level = 2, .Shares = 31337}

        nCompany.AddResource(craftRes)
        nCompany.AddResource(LuxRes)

        nCompany.Save()

        CompareFiles(outputFileExtension & nCompany.Name, outputFileExtension & "Expected_" & nCompany.Name)
    End Sub

    <TestMethod()>
    Public Sub SaveCompanyToFile_SaveCompanyWithResourceProduction_SingleResourceRequired()
        Dim nCompany As New Company(New AssetManager, New SaveCompanyToFile())

        nCompany.Name = "CompanyWithResourceProduction_SingleResourceRequired"
        nCompany.Level = 1
        nCompany.Shares = 100

        Dim settings As New EngineSettings()
        Dim production As New ResourceProduction(settings)
        production.RequiredResources.Add(New CraftResource() With {.Name = "TestRequired", .Shares = 2, .Level = 2})
        production.ProducedResource = New CraftResource() With {.Name = "OutputRes", .Shares = 1, .Level = 10}
        nCompany.ProducedResource = production

        nCompany.Save()

        CompareFiles(outputFileExtension & nCompany.Name, outputFileExtension & "Expected_" & nCompany.Name)
    End Sub

    <TestMethod()>
    Public Sub SaveCompanyToFile_SaveCompanyWithResourceProduction_ManyResourceRequired()
        Dim nCompany As New Company(New AssetManager, New SaveCompanyToFile())

        nCompany.Name = "CompanyWithResourceProduction_ManyResourceRequired"
        nCompany.Level = 1
        nCompany.Shares = 100

        Dim settings As New EngineSettings()
        Dim production As New ResourceProduction(settings)
        production.RequiredResources.Add(New CraftResource() With {.Name = "TestRequired", .Shares = 2, .Level = 2})
        production.RequiredResources.Add(New CraftResource() With {.Name = "TestRequired2", .Shares = 2, .Level = 2})
        production.ProducedResource = New CraftResource() With {.Name = "OutputRes", .Shares = 1, .Level = 10}
        nCompany.ProducedResource = production


        nCompany.Save()

        CompareFiles(outputFileExtension & nCompany.Name, outputFileExtension & "Expected_" & nCompany.Name)
    End Sub

    <TestMethod()>
    Public Sub SaveCompanyToFile_LoadCompanyByBase_Basic()
        Dim testCompany As Company = IPersistMarketForce.LoadMarketForce("Expected_SaveEmptyCompany", New SaveCompanyToFile(), Nothing)

        Assert.AreEqual("SaveEmptyCompany", testCompany.Name)
        Assert.AreEqual(100, testCompany.Shares)
        Assert.AreEqual(1, testCompany.Level)

    End Sub

    <TestMethod()>
    Public Sub SaveCompanyToFile_LoadCompanyByBase_CompanyWithAssets()
        Dim testCompany As Company = IPersistMarketForce.LoadMarketForce("Expected_CompanyWithAssets", New SaveCompanyToFile(), Nothing)

        Assert.AreEqual("CompanyWithAssets", testCompany.Name)
        Assert.AreEqual(100, testCompany.Shares)
        Assert.AreEqual(1, testCompany.Level)
        Assert.AreEqual(2, testCompany.GetAllAssets().Count)
        Assert.IsNotNull(testCompany.GetAsset("CraftResource"))
        Assert.IsNotNull(testCompany.GetAsset("LuxResource"))


    End Sub

    <TestMethod()>
    Public Sub SaveCompanyToFile_LoadCompanyByBase_CompanyWithResourceProduction_Simple()
        Dim testCompany As Company = IPersistMarketForce.LoadMarketForce("Expected_CompanyWithResourceProduction_SingleResourceRequired", New SaveCompanyToFile(), New EngineSettings())

        Assert.AreEqual("CompanyWithResourceProduction_SingleResourceRequired", testCompany.Name)
        Assert.AreEqual(100, testCompany.Shares)
        Assert.AreEqual(1, testCompany.Level)

    End Sub

    <TestMethod()>
    Public Sub SaveCompanyToFile_LoadCompanyByBase_CompanyWithResourceProduction_Complex()
        Dim testCompany As Company = IPersistMarketForce.LoadMarketForce("Expected_CompanyWithResourceProduction_ManyResourceRequired", New SaveCompanyToFile(), New EngineSettings())

        Assert.AreEqual("CompanyWithResourceProduction_ManyResourceRequired", testCompany.Name)
        Assert.AreEqual(100, testCompany.Shares)
        Assert.AreEqual(1, testCompany.Level)

    End Sub
    Private Sub CompareFiles(expectedFileName As String, resultFileName As String)
        Using expectedFile As FileStream = File.Open(expectedFileName, FileMode.Open)
            Using expectedReader As New StreamReader(expectedFile)

                Using resultFile As FileStream = File.Open(resultFileName, FileMode.Open)
                    Using resultReader As New StreamReader(resultFile)

                        While Not expectedReader.EndOfStream
                            If resultReader.EndOfStream Then Assert.Fail("Files have different number of lines.")
                            Dim expectedString As String = expectedReader.ReadLine()
                            Dim resultString As String = resultReader.ReadLine()

                            Assert.AreEqual(expectedString, resultString)

                        End While

                    End Using

                End Using
            End Using
        End Using
    End Sub

End Class