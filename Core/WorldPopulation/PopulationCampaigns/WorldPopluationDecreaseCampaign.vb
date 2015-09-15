
Public Class WorldPopluationIncreaseCampaign
    Implements IPopulationCampaign

    Dim random As Random
    Public Sub New(randomGenerator As Random)
        random = randomGenerator
    End Sub

    Public Sub Run(ByRef population As IList(Of Person)) Implements IPopulationCampaign.Run

        Dim populationIncreases As New Dictionary(Of Integer, Integer)


        For Each subject As Person In population
            Dim hadChild As Boolean = random.Next(0, 100) < (30 - subject.Income * 5)
            If hadChild Then
                If populationIncreases.ContainsKey(subject.Income) Then
                    populationIncreases(subject.Income) += 1
                Else
                    populationIncreases(subject.Income) = 1
                End If
            End If
        Next


        For Each incomeLevel As Integer In populationIncreases.Keys
            For counter As Integer = 0 To populationIncreases(incomeLevel)
                population.Add(New Person(incomeLevel))
            Next
        Next


    End Sub
End Class