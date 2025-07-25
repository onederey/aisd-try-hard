gcd ()	# a b -> res
{
	if [[ $2 -eq 0 ]];
	then
		echo "GCD is $1"
	else
		let n=$1%$2
		gcd $2 $n
	fi
}

while [[ 1 -eq 1  ]]
do
	read a b

	if [[ ! $a ]]
	then
		echo "bye"
		exit
	fi

	gcd $a $b
done
