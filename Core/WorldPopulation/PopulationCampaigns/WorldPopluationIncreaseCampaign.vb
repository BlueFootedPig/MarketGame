
Public Class WorldPopluationDecreaseCampaign
    Implements IPopulationCampaign

    Dim random As Random
    Public Sub New(randomGenerator As Random)
        random = randomGenerator
    End Sub

    Public Sub Run(ByRef population As IList(Of Person)) Implements IPopulationCampaign.Run

        Dim populationDecreases As New List(Of Person)


        For Each subject As Person In population
            Dim hasDied As Boolean = random.Next(0, 100) < (28 - subject.Income * 5)
            If hasDied Then
                populationDecreases.Add(subject)
            End If
        Next


        For Each subject As Person In populationDecreases
            population.Remove(subject)
        Next


    End Sub
End Class