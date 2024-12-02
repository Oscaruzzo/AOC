function ParseCubeSet([string] $CubeSet) {
    $set = @{}
    foreach ($cubes in $CubeSet.Split(',').Trim()) {
        $n, $c = $cubes.Split(' ')
        $set[$c] = [int] $n
    }
    return $set
}

function IsGoodSet([hashtable] $Set, [hashtable] $Reference) {
    foreach ($color in $Set.Keys) {
        if (($null -eq $Set[$color]) -or ($Reference[$color] -ge $Set[$color])) {
            # do nothing
        } else {
            return $false
        }
    }
    return $true
}

function GetMaxSet([hashtable] $Set1, [hashtable] $Set2) {
    $Res = $Set2.Clone()
    foreach ($color in $Set1.Keys) {
        if (($null -eq $Set2[$color]) -or ($Set1[$color] -ge $Set2[$color])) {
            $Res[$color] = $Set1[$color]
        } else {
            $Res[$color] = $Set2[$color]
        }
    }
    return $Res
}

function ParseGame([string] $Line) {
    $splitLine = $Line.Split(':')
    $header = $splitLine[0]
    $gameIndex = [int32] ($header | Select-String '[0-9]+').Matches.Value
    $gameData = $splitLine[1]

    $sets = @()
    foreach ($cubeSet in $gameData.Split(';').Trim()) {
        $sets += ParseCubeSet($cubeSet)
    }

    return $gameIndex, $sets
}

function EvaluateGame([string] $Line) {
    $gameIndex, $sets = ParseGame($Line)
    $maxSet = @{}
    foreach ($set in $sets) {
        $maxSet = GetMaxSet -Set1 $set -Set2 $maxSet
    }

    $p = 1
    foreach ($n in $maxSet.Values) {
        $p = $p * $n
    }
    return $p
}

$line = 'Game 17: 6 red, 13 blue, 8 green; 12 blue, 7 green, 9 red; 19 blue, 5 red; 2 green, 8 red, 14 blue'

EvaluateGame -Line $line

$n = 0
foreach ($line in Get-Content .\input2.txt) {
    $n += EvaluateGame -Line $line
}

$n