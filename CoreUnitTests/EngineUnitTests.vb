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
        Assert.Inconclusive()

    End Sub


End Class