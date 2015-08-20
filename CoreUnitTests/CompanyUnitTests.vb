Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()> Public Class CompanyUnitTests

    <TestMethod()>
    Public Sub Company_PerformSingleStrategy()
        Assert.Inconclusive()
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()

        'setup
        Dim testCompany As New Company()



        'test
        testCompany.PerformAction(mockMarket)

        'verify
        Assert.Inconclusive()
    End Sub

    <TestMethod()>
    Public Sub Company_AddResource()
        'setup
        Dim testCompany As New Company()
        Dim newResourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 1337}

        'test
        testCompany.AddResource(newResourceToAdd)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As Resource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(1337, resourceToCheck.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub Company_AddResource_NullResource()
        'setup
        Dim testCompany As New Company()

        'test
        testCompany.AddResource(Nothing)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Company_AddResource_NoShares()
        'setup
        Dim testCompany As New Company()
        Dim newResourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 0}

        'test
        testCompany.AddResource(newResourceToAdd)

    End Sub

    <TestMethod()>
    Public Sub Company_RemoveResource()
        'setup
        Dim testCompany As New Company()
        Dim newResourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 20}
        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 5}
        testCompany.AddResource(newResourceToAdd)
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As Resource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Company_RemoveResource_NoResource()
        'setup
        Dim testCompany As New Company()
        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 5}
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As Resource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Company_RemoveResource_NotEnough()
        'setup
        Dim testCompany As New Company()
        Dim newResourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 5}
        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 20}
        testCompany.AddResource(newResourceToAdd)
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As Resource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

End Class