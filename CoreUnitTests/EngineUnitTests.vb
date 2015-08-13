Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core

<TestClass()>
Public Class EngineUnitTests

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub Engine_NullMarket()
        Dim testEngine As New Engine(Nothing)

    End Sub

    <TestMethod>
    Public Sub Engine_()
        Dim mockMarket As IMarket = NSubstitute.Substitute.For(Of IMarket)()

        Dim testEngine As New Engine(mockMarket)

    End Sub

End Class