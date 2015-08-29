Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()>
Public Class EngineUnitTests

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub Engine_NullMarket()
        Dim testEngine As New Engine(Nothing)

    End Sub

    <TestMethod()>
    Public Sub Engine_ExecuteStrageties()
        'setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()
        Dim mockCompany As Company = Substitute.For(Of Company)()

        Dim testengine As New Engine(mockMarket)
        testengine.Companies.Add(mockCompany)

        'test
        testengine.ExecuteComputerActions()

        'verify
        mockCompany.ReceivedWithAnyArgs().PerformAction(Nothing)

    End Sub


    <TestMethod()>
    Public Sub Engine_ExecuteStrageties_ManyCompanies()
        'setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()
        Dim mockCompany As Company = Substitute.For(Of Company)()

        Dim testengine As New Engine(mockMarket)
        testengine.Companies.Add(mockCompany)
        Dim mockCompany2 = Substitute.For(Of Company)()
        testengine.Companies.Add(mockCompany2)
        Dim mockCompany3 = Substitute.For(Of Company)()
        testengine.Companies.Add(mockCompany3)


        'test
        testengine.ExecuteComputerActions()

        'verify
        mockCompany.ReceivedWithAnyArgs().PerformAction(Nothing)
        mockCompany2.ReceivedWithAnyArgs().PerformAction(Nothing)
        mockCompany3.ReceivedWithAnyArgs().PerformAction(Nothing)
    End Sub


End Class