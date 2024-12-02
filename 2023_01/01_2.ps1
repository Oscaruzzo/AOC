function ConvertTo-Number($x) {
    switch ($x) {
        'zero' {0}
        'one' {1}
        'two' {2}
        'three' {3}
        'four' {4}
        'five' {5}
        'six' {6}
        'seven' {7}
        'eight' {8}
        'nine' {9}
        Default {[int] $x}
    }
}

$In = Get-Content .\input1.txt
$tot = 0
$In | ForEach-Object {
    $nums = ($_ |  Select-String '[0-9]|zero|one|two|three|four|five|six|seven|eight|nine' -AllMatches).Matches.Value
    $m1 = ConvertTo-Number ($nums | Select-Object -First 1)
    $m2 = ConvertTo-Number ($nums | Select-Object -Last 1)
    # "$m1 --- $m2"
    $n = $m1 * 10 + $m2
    # $n
    $tot = $tot + $n
}

$tot