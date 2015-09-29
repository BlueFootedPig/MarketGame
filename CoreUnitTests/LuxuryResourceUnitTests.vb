Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core

'the shared tags needs to be fixed in order to test
<TestClass()>
<Ignore>
Public Class LuxuryResourceUnitTests

    <TestMethod()>
    Public Sub LuxuryResource_OneItemSingleTags()
        Dim testResource As New LuxuryResource
        testResource.AddPrefferedCustomer("TestTag")

        Assert.AreEqual(1, testResource.PrefferedCustomers.Count)
        Assert.AreEqual("TestTag", testResource.PrefferedCustomers.First())
        Assert.AreEqual(1, LuxuryResource.TotalTags)
    End Sub

    <TestMethod()>
    Public Sub LuxuryResource_OneItemMultipleTags()
        Dim testResource As New LuxuryResource
        testResource.AddPrefferedCustomer("TestTag")
        testResource.AddPrefferedCustomer("TestTag2")
        testResource.AddPrefferedCustomer("TestTag3")

        Assert.AreEqual(3, testResource.PrefferedCustomers.Count)
        Assert.AreEqual("TestTag", testResource.PrefferedCustomers.First())
        Assert.AreEqual("TestTag3", testResource.PrefferedCustomers.Last())
        Assert.AreEqual(3, LuxuryResource.TotalTags)
    End Sub

    <TestMethod()>
    Public Sub LuxuryResource_MultiItemSingleTags()
        Dim testResource As New LuxuryResource
        testResource.AddPrefferedCustomer("TestTag")
        testResource = New LuxuryResource()
        testResource.AddPrefferedCustomer("TestTag2")
        testResource = New LuxuryResource()
        testResource.AddPrefferedCustomer("TestTag3")

        Assert.AreEqual(1, testResource.PrefferedCustomers.Count)
        Assert.AreEqual("TestTag3", testResource.PrefferedCustomers.First())
        Assert.AreEqual(3, LuxuryResource.TotalTags)
    End Sub

    <TestMethod()>
    Public Sub LuxuryResource_MultiItemMultipleTags()
        Dim testResource As New LuxuryResource
        testResource.AddPrefferedCustomer("TestTag")
        testResource.AddPrefferedCustomer("TestTag2")
        testResource.AddPrefferedCustomer("TestTag3")
        testResource = New LuxuryResource()
        testResource.AddPrefferedCustomer("Tag")
        testResource.AddPrefferedCustomer("Tag2")
        testResource.AddPrefferedCustomer("Tag3")
        testResource = New LuxuryResource()
        testResource.AddPrefferedCustomer("Leet")
        testResource.AddPrefferedCustomer("Leet2")
        testResource.AddPrefferedCustomer("Leet3")

        Assert.AreEqual(3, testResource.PrefferedCustomers.Count)
        Assert.AreEqual("Leet", testResource.PrefferedCustomers.First())
        Assert.AreEqual("Leet3", testResource.PrefferedCustomers.Last())
        Assert.AreEqual(9, LuxuryResource.TotalTags)
    End Sub

End Class