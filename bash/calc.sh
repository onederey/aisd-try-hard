while true
do
	read a op b

	if [[ $a == "exit" ]]
	then
		echo "bye"
		break
	fi

	case $op in
	"+")
		echo $(($a+$b))
		;;
	"-")
		echo $(($a-$b))
		;;
	"*")
		echo $(($a*$b))
		;;
	"/")
		echo $(($a/%b))
		;;
	"%")
		echo $(($a%$b))
		;;
	"**")
		echo $(($a**$b))
		;;
	*)
		echo "error"
		break
	esac
done
